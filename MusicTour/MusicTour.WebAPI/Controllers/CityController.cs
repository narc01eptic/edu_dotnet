﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MusicTour.BLL.Contracts;
using MusicTour.Client.DTO.Read;
using MusicTour.Client.Requests.Create;
using MusicTour.Client.Requests.Update;
using MusicTour.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace MusicTour.WebAPI.Controllers
{
    [ApiController]
    [Route("api/cinema")]
    public class CityController
    {
        private ILogger<CityController> Logger { get; }
        private ICityCreateService CityCreateService { get; }
        private ICityGetService CityGetService { get; }
        private ICityUpdateService CityUpdateService { get; }
        private IMapper Mapper { get; }

        public CityController(ILogger<CityController> logger, IMapper mapper, ICityCreateService cinemaCreateService, ICityGetService cinemaGetService, ICityUpdateService cinemaUpdateService)
        {
            this.Logger = logger;
            this.CityCreateService = cinemaCreateService;
            this.CityGetService = cinemaGetService;
            this.CityUpdateService = cinemaUpdateService;
            this.Mapper = mapper;
        }

        [HttpPut]
        [Route("")]
        public async Task<CityDTO> PutAsync(CityCreateDTO cinema)
        {
            this.Logger.LogTrace($"{nameof(this.PutAsync)} called");

            var result = await this.CityCreateService.CreateAsync(this.Mapper.Map<CityUpdateModel>(cinema));

            return this.Mapper.Map<CityDTO>(result);
        }

        [HttpPatch]
        [Route("")]
        public async Task<CityDTO> PatchAsync(CityUpdateDTO cinema)
        {
            this.Logger.LogTrace($"{nameof(this.PutAsync)} called");

            var result = await this.CityUpdateService.UpdateAsync(this.Mapper.Map<CityUpdateModel>(cinema));

            return this.Mapper.Map<CityDTO>(result);
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<CityDTO>> GetAsync()
        {
            this.Logger.LogTrace($"{nameof(this.GetAsync)} called");

            return this.Mapper.Map<IEnumerable<CityDTO>>(await this.CityGetService.GetAsync());
        }

        [HttpGet]
        [Route("{cinemaId}")]
        public async Task<CityDTO> GetAsync(int cinemaId)
        {
            this.Logger.LogTrace($"{nameof(this.GetAsync)} called for {cinemaId}");

            return this.Mapper.Map<CityDTO>(await this.CityGetService.GetAsync(new CityIdentityModel(cinemaId)));
        }
    }
}