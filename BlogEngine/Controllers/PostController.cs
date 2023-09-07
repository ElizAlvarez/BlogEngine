using System;
using BlogEngine.Authorization;
using BlogEngine.Core.Entities;
using BlogEngine.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogEngine.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostController : ControllerBase
	{
		private IPostService _postService;

		public PostController(IPostService postService)
		{
			_postService = postService;
		}

        [Authorize(Policy = AuthorizationData.RoleWriterPolicy)]
        [HttpPost]
		public async Task<IActionResult> Create(Post post)
		{
			try
			{
				var response = await _postService.Create(post);
				return Ok(response);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

        [Authorize(Policy = AuthorizationData.AllRoles)]
        [HttpGet]
		public async Task<IActionResult> GetAllPublished()
		{
			try
			{
				var response = await _postService.GetAllPublished();
				return Ok(response);
			}
			catch(Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[Authorize(Policy = AuthorizationData.RoleWriterPolicy)]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllByUser(int userId)
        {
            try
            {
                var response = await _postService.GetAllByUser(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Policy = AuthorizationData.RoleEditorPolicy)]
        [HttpGet]
		[Route("GetPending")]
        public async Task<IActionResult> GetPendingForApproval()
        {
            try
            {
                var response = await _postService.GetAllPendingForApproval();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Policy = AuthorizationData.RoleWriterPolicy)]
        [HttpPut]
        [Route("Edit")]
		public async Task<IActionResult> Edit(Post post)
		{
            try
            {
                var response = await _postService.Edit(post);
                if (response == null)
                    return NotFound("The post was not found.");

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = AuthorizationData.RoleWriterPolicy)]
        [HttpPatch]
        [Route("Submit/{postId}")]
        public async Task<IActionResult> Submit(int postId)
        {
            try
            {
                var response = await _postService.Submit(postId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

