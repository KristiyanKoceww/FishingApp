import { React } from 'react'
import Post from "./Post"

const Posts = (props) => {
    return (
        <div className="container">
            {props.posts.map((post) => {
                return <Post {...post} />
            })}
        </div>
    );
}
export default Posts;