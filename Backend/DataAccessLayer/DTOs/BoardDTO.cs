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
        private string _email;
        public BoardDTO(string email)
        {
            this._email = email;
            _controller = new Controllers.BoardControl();
            _controller.Insert(this);
        }

        public string Email { get => _email; set => _email = value; }
    }
}
