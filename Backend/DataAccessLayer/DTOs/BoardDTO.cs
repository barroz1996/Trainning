namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    internal class BoardDTO
    {
        public const string BoardEmailColumnEmail = "Email";
        private readonly Controllers.BoardControl _controller;
        public BoardDTO(string email)
        {
            Email = email;
            _controller = new Controllers.BoardControl();
        }

        public string Email { get; set; }
    }
}
