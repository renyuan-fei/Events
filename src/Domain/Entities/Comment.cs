using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.ValueObjects;
using Domain.ValueObjects.Comment;

namespace Domain.Entities;

public class Comment : BaseAuditableEntity<CommentId>
{

}