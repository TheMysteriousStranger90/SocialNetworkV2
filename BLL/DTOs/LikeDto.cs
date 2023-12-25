﻿namespace BLL.DTOs;

public class LikeDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public bool IsLike { get; set; }
    public int SourceUserId { get; set; }
    public int TargetUserId { get; set; }
}