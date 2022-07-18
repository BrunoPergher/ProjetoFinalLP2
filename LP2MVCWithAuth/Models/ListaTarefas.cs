using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace LP2MVCWithAuth.Models
{
    public class ListaTarefas
    {
        public ListaTarefas()
        {
            this.Tarefas = new List<ListaTarefas>();
        }

        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Titulo { get; set; }

        [StringLength(1000)]
        public string? Descricao { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:MM}")]
        public DateTime DataEntrega { get; set; }

        public List<ListaTarefas> Tarefas { get; set; }

        public Location PontoDePartida { get; set; }

        public List<Location> Locations { get; set; }
    }
}
