import { useState, useEffect } from "react";

const useFetch = (url,initialValue) => {

    const[state,setState] = useState([]);
    const[isLoading,setIsLoading] = useState(false);

    useEffect(()=>{
        setIsLoading(true);
        fetch(url)
        .then(res=> res.json())
        .then(result => {
            console.log(result)
            setState(result);
            setIsLoading(false);
        });
    },[url]);

    return [
        state,
        isLoading,
    ];
};

export default useFetch;