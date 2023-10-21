using AuctionApp.Business.AuctionServices;
using AuctionApp.Domain.DTO.AuctionDTOs;
using AuctionApp.Domain.Enteties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public AuctionController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
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
                throw ex;
            }

        }
    }
}
