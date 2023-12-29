using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.ValueObjects;

namespace Domain.Entities;

public class Comment : BaseAuditableEntity<CommentId>
{

}