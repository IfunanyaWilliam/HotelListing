using AutoMapper;
using HotelListing.Contracts;
using HotelListing.DTOs;
using HotelListing.Models;
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
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _uniitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CountryController> _logger;

        public CountryController(IUnitOfWork uniitOfWork, 
                                 IMapper mapper,
                                 ILogger<CountryController> logger)
        {
            _uniitOfWork = uniitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                var countries = await _uniitOfWork.Countries.GetAllAsync();
                var countriesMap = _mapper.Map<IList<CountryDto>>(countries);
                return Ok(countriesMap);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetCountries)}");
                return StatusCode(500, "Inernal server Error. Please try agian later.");
            }
        }

        [HttpGet("countryId", Name = "GetCountry")]
        public async Task<IActionResult> GetCountry(int countryId)
        {
            try
            {
                var country = await _uniitOfWork.Countries.GetAsync(i => i.Id == countryId, new List<string> { "Hotels"} );
                var countryMap = _mapper.Map<CountryDto>(country);
                return Ok(countryMap);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetCountry)}");
                return StatusCode(500, "Inernal server Error. Please try agian later.");
            }
        }


        //[Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryDto countryDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attemp in {nameof(CreateCountry)}");
                return BadRequest(ModelState);
            }


            try
            {
                var country = _mapper.Map<Country>(countryDto);
                await _uniitOfWork.Countries.InsertAsync(country);
                await _uniitOfWork.SaveAsync();

                return CreatedAtRoute("GetCountry", new { countryId = country.Id }, country);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(CreateCountry)}");
                return StatusCode(500, "Inernal server Error. Please try agian later.");
            }
        }


        //[Authorize]
        [HttpPut("{countryId:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCountry(int countryId, [FromBody] UpdateCountryDto countryDto)
        {
            if (!ModelState.IsValid || countryId < 1)
            {
                _logger.LogError($"Invalid POST attemp in {nameof(UpdateCountry)}");
                return BadRequest(ModelState);
            }


            try
            {
                var countryToUpdate = await _uniitOfWork.Countries.GetAsync(i => i.Id == countryId);
                if (countryToUpdate == null)
                {
                    _logger.LogError($"Invalid POST attemp in {nameof(UpdateCountry)}");
                    return BadRequest("Country not Found");
                }

                _mapper.Map(countryDto, countryToUpdate);
                _uniitOfWork.Countries.Update(countryToUpdate);
                await _uniitOfWork.SaveAsync();

                return CreatedAtRoute("GetCountry", new { countryId = countryToUpdate.Id }, countryToUpdate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateCountry)}");
                return StatusCode(500, "Inernal server Error. Please try agian later.");
            }
        }


        [HttpDelete("{countryId:int}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCountry(int countryId)
        {
            if (countryId < 1)
            {
                _logger.LogError($"Invalid Delete attempt in {nameof(DeleteCountry)}");
                return BadRequest();
            }

            try
            {
                var country = await _uniitOfWork.Hotels.GetAsync(i => i.Id == countryId);
                if (country == null)
                {
                    _logger.LogError($"CountryId does not match any Hotel in {nameof(DeleteCountry)}");
                    return BadRequest("Country not Found");
                }

                //Delete all hotels associated with country
                var hotels = await _uniitOfWork.Hotels.GetAllAsync( c => c.CountryId == countryId);
                _uniitOfWork.Hotels.DeleteRange(hotels);

                //Finally delete country
                await _uniitOfWork.Countries.DeleteAsync(countryId);
                await _uniitOfWork.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(DeleteCountry)}");
                return StatusCode(500, "Inernal server Error. Please try agian later.");
            }

        }
    }
}
