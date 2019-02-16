using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RazzorPagesExample.Model
{
    public class Product
    {
         
        public int Id { get; set; }

        [Display(Name = "상품명")]
        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        [Display(Name="모델명")]
        [Required]
        [StringLength(50)]
        [Column("Model")]
        public string ModelName { get; set; }

        [Display(Name = "가격")]
        public int Price { get; set; }

        [Display(Name="첨부파일")]
        [StringLength(50)]
        public string AttachFile { get; set; }


    }
}
