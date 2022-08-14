using AutoMapper;
using HotelListing.Contracts;
using HotelListing.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IUnitOfWork _uniitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<HotelController> _logger;

        public HotelController(IUnitOfWork uniitOfWork,
                               IMapper mapper,
                               ILogger<HotelController> logger)
        {
            _uniitOfWork = uniitOfWork;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotels()
        {
            try
            {
                var hotels = await _uniitOfWork.Hotels.GetAllAsync();
                var hotelsMap = _mapper.Map<IList<HotelDto>>(hotels);
                return Ok(hotelsMap);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetHotels)}");
                return StatusCode(500, "Inernal server Error. Please try agian later.");
            }
        }

        [HttpGet("hotelId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotel(int hotelId)
        {
            try
            {
                var hotel = await _uniitOfWork.Hotels.GetAsync(i => i.Id == hotelId, new List<string> { "Country"});
                var hotelMap = _mapper.Map<HotelDto>(hotel);
                return Ok(hotelMap);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetHotel)}");
                return StatusCode(500, "Inernal server Error. Please try agian later.");
            }
        }

    }
}
