const logout = async () => {
    await fetch('https://localhost:44366/api/AppUsers/logout',
      {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        credentials: 'include',
      });
    }
      export default logout