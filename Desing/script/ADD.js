async function addItem() {
    const url = 'https://localhost:44331/api/items';
    const itemName = document.getElementById('name');
    const itemDescription = document.getElementById('description');
    fetch(url, {
        method: 'POST',
        body: JSON.stringify({
            Name: itemName,
            Description: itemDescription,
            Deleted: false
        })
    });
}