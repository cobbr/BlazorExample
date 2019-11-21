using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorExample.Models
{
    public class Listener
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; } = "test";
        [Required]
        public int ListenerTypeId { get; set; }
        public ListenerType ListenerType { get; set; }
    }
}
