export async function getLogout() {
    let url = `http://localhost:3030/users/logout`
    let method = {
        headers: {
            'X-Authorization':sessionStorage.getItem('token'),
            'Content-Type': 'aplication/json'
        },
        method: 'Get',
    }
    sessionStorage.removeItem('id');
    sessionStorage.removeItem('token');
    sessionStorage.removeItem('еmail');
     await fetch(url, method);
  
 
}