using Application.Features.Languages.Dtos.Languages;
using Application.Features.Languages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Commands.CreateLanguage
{
    public class CreateLanguageCommand: IRequest<CreatedLanguageDto>
    {
        public string? Name { get; set; }

        public class CreateLanguageCommandHandler : IRequestHandler<CreateLanguageCommand, CreatedLanguageDto>
        {
            ILanguageRepository _languageRepository;
            IMapper _mapper;
            LanguageBusinessRules _businessRules;

            public CreateLanguageCommandHandler(ILanguageRepository languageRepository, IMapper mapper, LanguageBusinessRules businessRules)
            {
                _languageRepository = languageRepository;
                _mapper = mapper;
                _businessRules = businessRules;
            }

            public async Task<CreatedLanguageDto> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
            {
                await _businessRules.LanguageNameCanNotBeDuplicatedWhenInserted(request.Name);
                

                Language mappedLanguage = _mapper.Map<Language>(request);
                Language createdLanguage = await _languageRepository.AddAsync(mappedLanguage);
                CreatedLanguageDto createdLanguageDto = _mapper.Map<CreatedLanguageDto>(createdLanguage);

                return createdLanguageDto;
            }
        }

        
    }
}
