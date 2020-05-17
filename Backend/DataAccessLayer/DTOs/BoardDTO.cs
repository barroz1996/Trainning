using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    class BoardDTO
    {
        public const string BoardEmailColumnEmail = "Email";
        private Controllers.BoardControl _controller;
        private string email;
        public BoardDTO(string email)
        {
            this.Email = email;
            _controller = new Controllers.BoardControl();
            _controller.Insert(this);
        }

        public string Email { get => email; set => email = value; }
    }
}
