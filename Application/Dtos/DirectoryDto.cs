﻿


namespace Application.Dtos
{
    public class DirectoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsPrivate { get; set; }
        public string UserName { get; set; }
        public string WorkspaceName { get; set; }

    }
}
