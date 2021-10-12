import React, { useState, useCallback, useEffect, useMemo } from 'react'
import Post from './Post'
import getData from './index'


const Posts = (props) => {
   
    const [posts, setPosts] = useState([])
  
    const getPosts = useCallback(async () => {
      const posts = await getData()
      setPosts(posts)
    },[])
  
    const renderPosts = useMemo(() => {
      return posts.map((post, index) => {
        return (
          <Post key={post.id} index={index} {...post} />
        )
      })
    }, [posts])
  
    useEffect(() => {
        getPosts()
    }, [])
  
    return (
      <div>
        {renderPosts}
      </div>
    )
  }
  
  export default Posts