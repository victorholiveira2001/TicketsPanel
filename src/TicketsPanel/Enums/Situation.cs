using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel.DataAnnotations;

namespace TicketsPanel.Enums
{
    public enum Situation
    {
        [Display(Name = "Sem atendente definido")]
        NotDefinedAttendent,
        [Display(Name = "Aguardando atendente")]
        WaitingForAttendent
    }
}
