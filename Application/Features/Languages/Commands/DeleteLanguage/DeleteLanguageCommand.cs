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

namespace Application.Features.Languages.Commands.DeleteLanguage
{
    public class DeleteLanguageCommand: IRequest<DeletedLanguageDto>
    {
        public int Id { get; set; }

        public class DeleteLanguageCommandHandler : IRequestHandler<DeleteLanguageCommand, DeletedLanguageDto>
        {

            ILanguageRepository _languageRepository;
            IMapper _mapper;

            public DeleteLanguageCommandHandler(ILanguageRepository languageRepository, IMapper mapper, LanguageBusinessRules businessRules)
            {
                _languageRepository = languageRepository;
                _mapper = mapper;
            }

            public async Task<DeletedLanguageDto> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
            {

                Language language = await _languageRepository.GetAsync(l => l.Id == request.Id);

                Language deletedLanguage = await _languageRepository.DeleteAsync(language);

                DeletedLanguageDto deletedLanguageDto = _mapper.Map<DeletedLanguageDto>(deletedLanguage);

                return deletedLanguageDto;
            }
        }

    }
}
