using BusinessServiceTemplate.Core.Dtos;
using MediatR;

namespace BusinessServiceTemplate.Core.Requests
{
    public class GetTestSelectionsBySpecialityRequest : IRequest<IList<TestSelectionDto>>
    {
        public GetTestSelectionsBySpecialityRequest(int specialityId)
        {
            SpecialityId = specialityId;
        }

        public int SpecialityId { get; set; }
    }
}
