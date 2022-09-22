using Application.Features.Languages.Dtos.Languages;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Commands.UpdateLanguage
{
    public class UpdateLanguageCommand: IRequest<UpdatedLanguageDto>
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public class UpdateLanguageCommandHandler : IRequestHandler<UpdateLanguageCommand, UpdatedLanguageDto>
        {

            ILanguageRepository _languageRepository;
            IMapper _mapper;

            public UpdateLanguageCommandHandler(ILanguageRepository languageRepository, IMapper mapper)
            {
                _languageRepository = languageRepository;
                _mapper = mapper;
            }

            public async Task<UpdatedLanguageDto> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
            {
                Language language = await _languageRepository.GetAsync(l => l.Id == request.Id);

                language.Name = request.Name;

                Language updatedLanguage = await _languageRepository.UpdateAsync(language);

                UpdatedLanguageDto updatedLanguageDto = _mapper.Map<UpdatedLanguageDto>(updatedLanguage);

                return updatedLanguageDto;
            }
        }
    }
}
