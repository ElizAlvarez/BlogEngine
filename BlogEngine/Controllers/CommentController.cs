using System;
using BlogEngine.Core.Entities;
using BlogEngine.Core.Interfaces;
using BlogEngine.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	public class CommentController : ControllerBase
	{
		private ICommentService _commentService;

		public CommentController(ICommentService commentService)
		{
			_commentService = commentService;
		}

        [HttpPost]
        public async Task<IActionResult> Create(Comment comment)
        {
            try
            {
                var response = await _commentService.Create(comment);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetAllByPost(int postId)
        {
            try
            {
                var response = await _commentService.GetAllByPost(postId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

