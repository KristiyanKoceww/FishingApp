import './Post.css';


const post = (props) =>{

  return(
   
<div className="main">
<div class="container bootstrap snippets bootdey">
<div class="col-md-8">
  <div class="box box-widget">
    <div class="box-header with-border">
      <div class="user-block">
        <img class="img-circle" src="https://bootdey.com/img/Content/avatar/avatar1.png" alt="User Image"/>
        <span class="username"><a href="#">{props.User.FirstName}</a></span>
        <span class="description">Shared publicly - {props.CreatedOn}</span>
      </div>
      
    </div>
    <div class="box-body">
      <p>{props.Content}</p>

      <div class="attachment-block clearfix">
        {/* <img class="attachment-img" src={props.ImageUrls[0].ImageUrl} alt="Attachment Image"/> */}
        <div class="attachment-pushed">
        </div>
      </div>
      <button type="button" class="btn btn-default btn-xs"><i class="fa fa-thumbs-o-up"></i> Like</button>
      <button type="button" class="btn btn-default btn-xs"><i class="fa fa-thumbs-o-up"></i> Dislike</button>
      <button type="button" class="btn btn-default btn-xs"><i class="fa fa-share"></i> Share</button>
      <span class="pull-right text-muted">{props.Votes.Count ? props.Votes.Count : 0} likes - {props.Comments.Lenght ?  props.Comments.CoLenghtunt : 0} comments</span>
    </div>
    
    <div class="box-footer box-comments">
      <div class="box-comment">
        <img class="img-circle img-sm" src="https://bootdey.com/img/Content/avatar/avatar5.png" alt="User Image"/>
        <div class="comment-text">
          <span class="username">
            User writed this comment : 
           {/* {props.Comments[3].User.FirstName} - {props.Comments[3].User.LastName}  */}
          <span class="text-muted pull-right">{props.Comments.CreatedOn}</span>
          </span>
          {props.Comments.Content}
        </div>
      </div>
     
    </div>
    <div class="box-footer">
      <form action="#" method="post">
        <img class="img-responsive img-circle img-sm" src="https://bootdey.com/img/Content/avatar/avatar1.png" alt="Alt Text"/>
        <div class="img-push">
          <input type="text" class="form-control input-sm" placeholder="Press enter to post comment"/>
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