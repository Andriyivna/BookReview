using System.Collections.Generic;

namespace API.Dtos
{
    public class VirtualLibraryDto
    {
        public IReadOnlyList<VirtualBookReturnDto> Books { get; set; }
    }
}
