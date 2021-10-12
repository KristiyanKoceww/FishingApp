const getData = async () => {
    const dataPromise = await fetch('https://localhost:44366/api/Posts/getAllPosts');
    const data = await dataPromise.json();

    console.log(data);
    return data;
}

export default getData;