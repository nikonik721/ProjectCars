using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjectCars.BL.Interface;
using ProjectCars.BL.Services;
using ProjectCars.Controllers;
using ProjectCars.DL.Interface;
using ProjectCars.Extensions;
using ProjectCars.Models.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ProjectCars.Test
{
    public class RVTests
	{
		private IMapper _mapper;
		private Mock<IRVRepository> _rvRepository;
		private IRVService _rvService;
		private RVController _controller;

		IList<RV> _rv = new List<RV>()
			{
				{ new RV {

					RVid = 20,
				RVBrand = "Gemini AWD",
				RVModel = "23TE",
				DateOfManufacturing = new DateTime(2021, 11, 09),
				RVColor = "Champagne/ Seminole Red",
				Engine = "3.5L V6 EcoBoost® Turbo, 310HP",
				Fuel = "Gasoline"

				} },
				{ new RV {

					RVid = 18,
				RVBrand = "Gemini AWD",
				RVModel = "23TW",
				DateOfManufacturing = new DateTime(2021, 11, 09),
				RVColor = "Champagne/ Electric Blue",
				Engine = "3.5L V6 EcoBoost® Turbo, 310HP",
				Fuel = "Gasoline"

				} },
			};

		public RVTests()
		{
			var mockMapper = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new AutoMapping());
			});

			_mapper = mockMapper.CreateMapper();

			_rvRepository = new Mock<IRVRepository>();


			_rvService = new RVService(_rvRepository.Object);

			//inject
			_controller = new RVController(_rvService, _mapper);
		}

		[Fact]
		public async Task RV_GetAll_Count_Check()
		{
			//setup
			var expectedCount = 2;

			_rvRepository.Setup(x => x.GetAll())
				.ReturnsAsync(_rv);

			//Act
			var result = await _controller.GetAll();

			//Assert
			var okObjectResult = result as OkObjectResult;
			Assert.NotNull(okObjectResult);

			var positions = okObjectResult.Value as IEnumerable<RVResponse>;

			Assert.NotNull(positions);
			Assert.Equal(expectedCount, positions.Count());
		}

		[Fact]
		public async Task RV_Update_RVEngine()
		{
			//setup
			var RVid = 20;
			var RVEngine = "3.5L V6 EcoBoost® Turbo, 310HP";

			var position = _rv.FirstOrDefault(x => x.RVid == RVid);
			position.Engine = RVEngine;


			_rvRepository.Setup(x => x.Update(It.IsAny<RV>())).Callback(() =>
			{
				position.Engine = RVEngine;
			}).Returns(() => Task<RV>.Factory.StartNew(() => new RV()
			{
				RVid = position.RVid,
				RVBrand = position.RVBrand,
				RVModel = position.RVModel,
				DateOfManufacturing = position.DateOfManufacturing,
				RVColor = position.RVColor,
				Engine = position.Engine,
				Fuel = position.Fuel

			}));

			//Act
			var result = await _controller.Update(_mapper.Map<RVRequest>(position));

			//Assert
			var okObjectResult = result as OkObjectResult;
			Assert.NotNull(okObjectResult);

			var pos = okObjectResult.Value as RVResponse;
			Assert.NotNull(pos);
			Assert.Equal(RVEngine, pos.Engine);
		}

		[Fact]
		public async Task RV_Delete_Existing_RVid()
		{
			//setup
			var RVid = 20;

			var position = _rv.FirstOrDefault(x => x.RVid == RVid);


			_rvRepository.Setup(x => x.GetById(RVid)).ReturnsAsync(_rv.FirstOrDefault(x => x.RVid == RVid));
			_rvRepository.Setup(x => x.Delete(RVid)).Callback(() => _rv.Remove(position));

			//Act
			var result = await _controller.Delete(RVid);

			//Assert
			var okObjectResult = result as OkObjectResult;
			Assert.Null(okObjectResult);

			Assert.Null(_rv.FirstOrDefault(x => x.RVid == RVid));
		}

		[Fact]
		public async Task Laptop_Delete_NotExisting_RVid()
		{
			//setup
			var RVid = 3;

			var position = _rv.FirstOrDefault(x => x.RVid == RVid);


			_rvRepository.Setup(x => x.Delete(RVid)).Callback(() => _rv.Remove(position));

			//Act
			var result = await _controller.Delete(RVid);

			//Assert
			var notFoundObjectResult = result as NotFoundObjectResult;
			Assert.Null(notFoundObjectResult);

			Assert.Null(_rv.FirstOrDefault(x => x.RVid == RVid));
		}

		[Fact]
		public async Task RV_Create_RV()
		{
			//setup
			var position = new RV()
			{
				RVid = 26,
				RVBrand = "MAGNITUDE SUPER C",
				RVModel = "SV34",
				DateOfManufacturing = new DateTime(2021, 01, 01),
				RVColor = "Silver Springs",
				Engine = "6.7L POWER STROKE® V8 TURBO",
				Fuel = "Diesel"

			};

			_rvRepository.Setup(x => x.Create(It.IsAny<RV>())).Callback(() =>
			{
				_rv.Add(position);
			}).Returns(() => Task<RV>.Factory.StartNew(() => new RV()
			{
				RVid = 26,
				RVBrand = "FOUR WINDS SPRINTER",
				RVModel = "24BL",
				DateOfManufacturing = new DateTime(2021, 01, 03),
				RVColor = "Firecracker Red",
				Engine = "3.0L V6",
				Fuel = "Diesel"

			}));

			//Act
			var result = await _controller.Create(_mapper.Map<RVRequest>(position));

			//Assert
			var okObjectResult = result as OkObjectResult;
			Assert.NotNull(okObjectResult);

			var pos = okObjectResult.Value as RVResponse;
			Assert.NotNull(pos);

			Assert.NotNull(_rv.FirstOrDefault(x => x.RVid == position.RVid));
		}
	}
}
