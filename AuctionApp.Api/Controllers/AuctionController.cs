using AuctionApp.Business.AuctionServices;
using AuctionApp.Domain.DTO.AuctionDTOs;
using AuctionApp.Domain.Enteties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionService _auctionService;
        private readonly ILogger<AccountController> _logger;

        public AuctionController(IAuctionService auctionService, ILogger<AccountController> logger)
        {
            _auctionService = auctionService;
            _logger = logger;
        }

        [HttpGet("getAllAuctionsByUserId")]
        public List<AuctionDTO> GetAllAuctionsByUserId(int id)
        {
            try
            {
                return _auctionService.GetAllAuctionsByUserId(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while GetAllAuctionsByUserId.");

                throw ex;
            }
        }
        [HttpGet("getTimeAscAuctions")]
        public List<AuctionDTO> GetTimeAscAuctions()
        {
            try
            {
                return _auctionService.GetTimeAscAuctions();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while GetTimeAscAuctions.");
                throw ex;
            }
        }

        [HttpGet("getAuctionById")]
        public async Task<IActionResult> GetAuctionById(int id)
        {
            try
            {
                var result = await _auctionService.GetAuctionById(id);

                if (result != null)
                {
                    return Ok(result);
                }

                return BadRequest("Error: Could not get the auction");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getAuctionById.");

                throw ex;
            }

        }

        [HttpPost("createAuction")]
        public async Task<IActionResult> CreateAuction(CreateAuctionDTO createAuctionDTO)
        {
            try
            {
                var result = await _auctionService.CreateAuction(createAuctionDTO);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while CreateAuction.");

                throw ex;
            }

        }

        [HttpPost("bidAuction")]
        public async Task<IActionResult> BidAuction(int userId, int auctionId, decimal ammount)
        {
            try
            {
                var result = await _auctionService.BidAuction(userId, auctionId, ammount);

                if (result != null)
                {
                    return Ok(result);
                }

                return BadRequest("Error: Could not bid in the auction");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while BidAuction.");

                throw ex;
            }

        }

        [HttpDelete("deleteAuction")]
        public async Task<IActionResult> DeleteAuction(int id)
        {
            try
            {
                var result = _auctionService.DeleteAuction(id);

                if (result == true)
                {
                    return Ok("Auction deleted");
                }

                return BadRequest("Error: Could not delete auction");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while DeleteAuction.");

                throw ex;
            }

        }
    }
}
