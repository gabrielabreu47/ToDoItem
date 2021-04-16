async function getAll() {
    const url = 'https://localhost:44331/api/items';
    let listOfItemsOData = await fetch(url)
        .then(response => response.json());

    let listOfItems = listOfItemsOData.value;
    const tableContainer = document.getElementById('display');
    for (let index = 0; index < listOfItems.length; index++) {
        let tr = CreateRow(listOfItems[index]);
        tableContainer.appendChild(tr);
    }
    console.log(listOfItems);
}

function CreateField(currentItem, propertyItem) {
    let field = document.createTextNode(currentItem[propertyItem]);
    const td = document.createElement('td');
    td.appendChild(field);
    return td;
}

function CreateRow(object) {
    const tr = document.createElement('tr');
    const currentItem = object;
    tr.id = currentItem.Id;
    for (let property in currentItem) {
        let td = CreateField(currentItem, property)
        tr.appendChild(td);
    }
    return tr;
}