using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServiceTemplate.Test.Handlers
{
    public class CreatePanelHandlerTests
    {
        [Fact]
        public Task WhenClientTriggeringPanelCreate_ThenNewPanelAdded_ReturnTheCreatedPanel() 
        { 

            return Task.CompletedTask;
        }
    }
}
