using AutoMapper;
using FluentValidation;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoItem2.Model.Entities;
using TodoItem2.Services.Items;
using ToDoItem2.BL.Dtos;
using ToDoItem2.Models;

namespace ToDoItem2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult GetAll()
        {
            var items = _itemService.GetAll();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _itemService.GetById(id);
            if (item is null)
                return NotFound();

            return Ok(item);
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Create(ItemDto item)
        {
            var itemResult = _itemService.Create(item);
            if (!itemResult.IsValid)
                return BadRequest(itemResult.Errors); 

            return Created("", itemResult.Entity);
        }
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute]int id,[FromBody] ItemDto item)
        {
            var itemResult = _itemService.Update(id, item);
            if (!itemResult.IsValid)
                return BadRequest(itemResult.Errors);

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var itemResult = _itemService.Delete(id);
            if (!itemResult.IsValid)
                return BadRequest(itemResult.Errors);
            return Ok(itemResult.Entity);
        }
    }
}
