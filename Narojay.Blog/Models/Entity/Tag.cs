﻿using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Narojay.Blog.Models.Entity
{
    [Table("tags")]
    public class Tag :BaseEntity   
    {

        protected Tag()
        {
                
        }

        public Tag(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public string Name { get; set; }

    }
}