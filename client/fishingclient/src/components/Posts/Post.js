import './Post.css';
const post = (props) =>{

  return(
<div className="main">
<div className="container bootstrap snippets bootdey">
<div className="col-md-8">
  <div className="box box-widget">
    <div className="box-header with-border">
      <div className="user-block">
        <img className="img-circle" src="https://bootdey.com/img/Content/avatar/avatar1.png" alt="User"/>
        <span className="username"><a>{props.User.FirstName}</a></span>
        <span className="description">Shared publicly - {props.CreatedOn}</span>
      </div>
      
    </div>
    <div className="box-body">
      <p>{props.Content}</p>

      <div className="attachment-block clearfix">
        {/* <img className="attachment-img" src={props.ImageUrls[0].ImageUrl} alt="Attachment Image"/> */}
        <div className="attachment-pushed">
        </div>
      </div>
      <button type="button" className="btn btn-default btn-xs"><i className="fa fa-thumbs-o-up"></i> Like</button>
      <button type="button" className="btn btn-default btn-xs"><i className="fa fa-thumbs-o-up"></i> Dislike</button>
      <button type="button" className="btn btn-default btn-xs"><i className="fa fa-share"></i> Share</button>
      <span className="pull-right text-muted">{props.Votes.Count ? props.Votes.Count : 0} likes - {props.Comments.Lenght ?  props.Comments.CoLenghtunt : 0} comments</span>
    </div>
    
    <div className="box-footer box-comments">
      <div className="box-comment">
        <img className="img-circle img-sm" src="https://bootdey.com/img/Content/avatar/avatar5.png" alt="User"/>
        <div className="comment-text">
          <span className="username">
            User writed this comment : 
           {/* {props.Comments[3].User.FirstName} - {props.Comments[3].User.LastName}  */}
          <span className="text-muted pull-right">{props.Comments.CreatedOn}</span>
          </span>
          {props.Comments.Content}
        </div>
      </div>
     
    </div>
    <div className="box-footer">
      <form action="#" method="post">
        <img className="img-responsive img-circle img-sm" src="https://bootdey.com/img/Content/avatar/avatar1.png" alt="Alt Text"/>
        <div className="img-push">
          <input type="text" className="form-control input-sm" placeholder="Press enter to post comment"/>
        </div>
      </form>
    </div>
  </div>
</div>
</div>
</div>
  )
}

export default post;