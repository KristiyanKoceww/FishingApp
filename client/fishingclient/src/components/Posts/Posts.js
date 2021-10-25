import React, { useMemo } from 'react'
import Post from './Post'
import useFetch from "../../customHooks/useFetch";

const Posts = (props) => {
  const [posts, isPostLoading] = useFetch(`https://localhost:44366/api/Posts/getAllPosts`, {});

  const renderPosts = useMemo(() => {
    return posts.map((post, index) => {
      return (
        <Post key={post.id} index={index} {...post} />
      )
    })
  }, [posts])

  return (
    <div>
      {renderPosts}
    </div>
  )
}

export default Posts