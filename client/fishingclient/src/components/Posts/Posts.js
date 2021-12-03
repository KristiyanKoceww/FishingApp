import { React, useState } from 'react'
import { Button } from 'primereact/button'
import Post from "./Post"
import CreatePost from './CreatePost';
import './Posts.css'

import 'primereact/resources/themes/lara-light-indigo/theme.css';
import "primereact/resources/primereact.min.css";
import 'primeicons/primeicons.css';

const Posts = (props) => {
    const [createFormToggle, setCreateFormToggle] = useState(false);

    const toggleForm = () => {
        setCreateFormToggle(!createFormToggle);
    }

    return (
        <div className="container">
            <div className="button">
                <Button className='p-button-primary' icon='pi pi-plus' label="Add post" icon="pi pi-check" iconPos="right" onClick={toggleForm} />
                {
                    createFormToggle &&
                    <CreatePost onCreate={props.updatePosts} />
                }
            </div>

            {props.posts.map((post) => {
                return <Post {...post} />
            })}
        </div>
    );
}
export default Posts;