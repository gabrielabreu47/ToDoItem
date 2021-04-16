using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoItem2.Core;
using TodoItem2.Model.Entities;
using TodoItem2.Model.Repositories;
using ToDoItem2.BL.Dtos;

namespace TodoItem2.Services.Items
{
    public interface IItemService
    {
        IEnumerable<ItemDto> GetAll();
        ItemDto GetById(int id);
        IEntityValidatorResult<ItemDto> Create(ItemDto itemDto);
        IEntityValidatorResult<ItemDto> Update(int id,ItemDto itemDto);
        IEntityValidatorResult<ItemDto> Delete(int id);
    }

    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<ItemDto> _validator;

        public ItemService(IItemRepository  itemRepository, IMapper mapper, IValidator<ItemDto> validator)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
            _validator = validator;
        }
        public IEnumerable<ItemDto> GetAll()
        {
            var items = _itemRepository.GetAll();
            var itemDtos = _mapper.Map<IEnumerable<ItemDto>>(items);
            return itemDtos;
        }
        public ItemDto GetById(int id)
        {
            var item = _itemRepository.GetById(id);
            var itemDto = _mapper.Map<ItemDto>(item);
            return itemDto;
        }
        public IEntityValidatorResult<ItemDto> Create(ItemDto itemDto)
        {
            var validationResult = _validator.Validate(itemDto);
            if (!validationResult.IsValid)
                return new EntityValidatorResult<ItemDto>
                {
                    Entity = itemDto,
                    IsValid = false,
                    Errors = validationResult.Errors.Select(x => x.ErrorMessage)
                };

            var isItemCreated = _itemRepository.GetAll()
                .Any(x=> x.Name.Equals(itemDto.Name));

            if(isItemCreated)
                return new EntityValidatorResult<ItemDto>
                {
                    Entity = itemDto,
                    IsValid = false,
                    Errors = new List<string>
                    {
                        "This item already exist"
                    }
                };

            var item = _mapper.Map<Item>(itemDto);
            item = _itemRepository.Create(item);
            itemDto = _mapper.Map<ItemDto>(item);

            return new EntityValidatorResult<ItemDto>
                {
                    Entity = itemDto,
                    IsValid = true
                };
        }
        public IEntityValidatorResult<ItemDto> Update(int id, ItemDto itemDto)
        {
            var validationResult = _validator.Validate(itemDto);
            if (!validationResult.IsValid)
                return new EntityValidatorResult<ItemDto>
                {
                    Entity = itemDto,
                    IsValid = false,
                    Errors = validationResult.Errors.Select(x => x.ErrorMessage)
                };

            var isItemCreated = _itemRepository.GetAll()
                .Where(x => x.Id != id)
                .Any(x => x.Name.Equals(itemDto.Name));

            if (isItemCreated)
                return new EntityValidatorResult<ItemDto>
                {
                    Entity = itemDto,
                    IsValid = false,
                    Errors = new List<string>
                    {
                        "This item already exist"
                    }
                };

            var item = _itemRepository.GetById(id);
            item = _itemRepository.Update(item);
            itemDto = _mapper.Map<ItemDto>(item);

            return new EntityValidatorResult<ItemDto>
            {
                Entity = itemDto,
                IsValid = true
            };
        }
        public IEntityValidatorResult<ItemDto> Delete(int id)
        {
            var item = _itemRepository.GetById(id);

            if (item != null)
                return new EntityValidatorResult<ItemDto>
                {
                    Entity = _mapper.Map<ItemDto>(item),
                    IsValid = false,
                    Errors = new List<string>
                    {
                        "This item already deleted"
                    }
                };

            item = _itemRepository.Delete(id);
            var itemDto = _mapper.Map<ItemDto>(item);
            return new EntityValidatorResult<ItemDto>
            {
                Entity = itemDto,
                IsValid = true
            };
        }
    }
}
