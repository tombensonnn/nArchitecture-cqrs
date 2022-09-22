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

namespace Application.Features.Languages.Queries.GetByIdLanguage
{
    public class GetByIdLanguageQuery: IRequest<GetByIdLanguageDto>
    {
        public int Id { get; set; }

        public class GetByIdLanguageQueryHandler : IRequestHandler<GetByIdLanguageQuery, GetByIdLanguageDto>
        {
            ILanguageRepository _languageRepository;
            IMapper _mapper;
            LanguageBusinessRules _businessRules;

            public GetByIdLanguageQueryHandler(ILanguageRepository languageRepository, IMapper mapper, LanguageBusinessRules businessRules)
            {
                _languageRepository = languageRepository;
                _mapper = mapper;
                _businessRules = businessRules;
            }

            public async Task<GetByIdLanguageDto> Handle(GetByIdLanguageQuery request, CancellationToken cancellationToken)
            {
                Language? language = await _languageRepository.GetAsync(l => l.Id == request.Id);

                _businessRules.LanguageShouldExistWhenRequested(language);

                GetByIdLanguageDto getByIdLanguageDto = _mapper.Map<GetByIdLanguageDto>(language);

                return getByIdLanguageDto;

             }
        }
    }
}
