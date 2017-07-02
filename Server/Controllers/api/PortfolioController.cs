using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoxMoney.Server.Entities;
using FoxMoney.Server.Repositories;
using FoxMoney.Server.Repositories.Abstract;
using FoxMoney.Server.Services;
using FoxMoney.Server.ViewModels;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FoxMoney.Server.Controllers.api {

    //[Authorize]
    [Route("api/[controller]")]
    public class PortfolioController : BaseController {

        private readonly IMapper mapper;
        private readonly PortfolioRepository repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public PortfolioController(IMapper mapper, PortfolioRepository repository, UserManager<ApplicationUser> userManager) {
            this.repository = repository;
            this.mapper = mapper;
            this._userManager = userManager;
        }

        [HttpGet("")]
        public async Task<IEnumerable<PortfolioViewModel>> GetPortfolios() {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            //var portfolios = await repository.GetAllAsync();
            var portfolios = await repository.FindByAsync(p => p.Owner == user);

            return mapper.Map<IEnumerable<Portfolio>, IEnumerable<PortfolioViewModel>>(portfolios);
        }

        [HttpGet("{id}")]
        public async Task<PortfolioViewModel> GetPortfolio(int id) {
            //var portfolio = await repository.GetSingleAsync(id);
            //var portfolio = repository.GetSingle((p => p.Id == id), (p => p.Holdings));
            var portfolio = repository.GetDetailedPortfolio(id);

            return mapper.Map<Portfolio, PortfolioDetailViewModel>(portfolio);
        }

        [HttpGet("{id}/removeDraft")]
        public async Task<IActionResult> RemoveDraft(int id) {
            var portfolio = await repository.GetSingleAsync(id);
            portfolio.Draft = false;
            repository.Edit(portfolio);
            repository.Commit();

            BackgroundJob.Enqueue<BackgroundProcessingService>(service =>
                service.UpdatePortfolioTotals(id));

            return Ok();
        }

        [HttpGet("{id}/recalculateTotals")]
        public async Task<IActionResult> RecalculateTotals(int id) {
            BackgroundJob.Enqueue<BackgroundProcessingService>(service =>
                service.UpdatePortfolioTotals(id));

            return Ok();
        }

        [HttpPost("")]
        public async Task<IActionResult> CreatePortfolio([FromBody] SavePortfolioViewModel portfolioViewModel) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var portfolio = mapper.Map<SavePortfolioViewModel, Portfolio>(portfolioViewModel);

            //set some defaults
            //portfolio.Active = false;
            //portfolio.Draft = true;

            repository.Add(portfolio);
            repository.CommitWithWait();

            portfolio = await repository.GetSingleAsync(portfolio.Id);

            BackgroundJob.Enqueue<BackgroundProcessingService>(service =>
                service.ProcessNewPortfolioComponents(portfolio, portfolioViewModel));

            var result = mapper.Map<Portfolio, PortfolioViewModel>(portfolio);

            return Ok(result);
        }
    }
}