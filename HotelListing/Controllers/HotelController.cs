using AutoMapper;
using HotelListing.Contracts;
using HotelListing.DTOs;
using HotelListing.Models;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet("{hotelId:int}", Name = "GetHotel")]
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

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelDto hotelDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attemp in {nameof(CreateHotel)}");
                return BadRequest(ModelState);
            }
                

            try
            {
                var hotel = _mapper.Map<Hotel>(hotelDto);
                await _uniitOfWork.Hotels.InsertAsync(hotel);
                await _uniitOfWork.SaveAsync();

                return CreatedAtRoute("GetHotel", new { hotelId = hotel.Id }, hotel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(CreateHotel)}");
                return StatusCode(500, "Inernal server Error. Please try agian later.");
            }
        }

        //[Authorize]
        [HttpPut("{hotelId:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateHotel(int hotelId, [FromBody] UpdateHotelDto hotelDto)
        {
            if (!ModelState.IsValid || hotelId < 1)
            {
                _logger.LogError($"Invalid POST attemp in {nameof(UpdateHotel)}");
                return BadRequest(ModelState);
            }


            try
            {
                var hotelToUpdate = await _uniitOfWork.Hotels.GetAsync(i => i.Id == hotelId);
                if(hotelToUpdate == null)
                {
                    _logger.LogError($"Invalid POST attemp in {nameof(UpdateHotel)}");
                    return BadRequest("Hotel not Found");
                }

                _mapper.Map(hotelDto, hotelToUpdate);
                _uniitOfWork.Hotels.Update(hotelToUpdate);
                await _uniitOfWork.SaveAsync();

                return CreatedAtRoute("GetHotel", new { hotelId = hotelToUpdate.Id }, hotelToUpdate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateHotel)}");
                return StatusCode(500, "Inernal server Error. Please try agian later.");
            }
        }
    }
}
