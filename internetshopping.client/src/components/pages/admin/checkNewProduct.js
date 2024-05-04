import axios from 'axios';

const checkProduct = async (newProduct) => {
    newProduct.category = await checkCategory(newProduct.category);
    newProduct.supplier = await checkSupplier(newProduct.supplier);

    return newProduct;
}
export default checkProduct;

const checkCategory = async (category) => {
    try {
        const categories = await getAllCategories();
        var catogoryEl = categories.find(a => compareNames(a.title, category));
        if (catogoryEl) {
            return catogoryEl.id;
        } else {
            const newCategory = { title: category };
            await addCategory(newCategory);
            const categories = await getAllCategories();
            var catogoryEl = categories.find(a => compareNames(a.title, category));
            return catogoryEl.id;
        }
    } catch (error) {
        console.error(error);
    }
};

const checkSupplier = async (supplier) => {
    try {
        const suppliers = await getAllSuppliers();
        var supplierEl = suppliers.find(a => compareNames(a.title, supplier));
        if (supplierEl) {
            return supplierEl.id;
        } else {
            const newSupplier = { title: supplier };
            await addSupplier(newSupplier);
            const suppliers = await getAllSuppliers();
            var supplierEl = suppliers.find(a => compareNames(a.title, supplier));
            return supplierEl.id;
        }
    } catch (error) {
        console.error(error);
    }
};

async function getAllCategories() {
    try {
        const categories = await axios.get('/category');
        return categories.data;
    } catch (error) {
        console.error(error);
        return [];
    }
}

async function getAllSuppliers() {
    try {
        const suppliers = await axios.get('/supplier');
        return suppliers.data;
    } catch (error) {
        console.error(error);
        return [];
    }
}

const addCategory = async (category) => {
    try {
        const response = await axios.post('/category/add_category', category);
        return response.data;
    } catch (error) {
        console.error(error);
    }
};

const addSupplier = async (supplier) => {
    try {
        const response = await axios.post('/supplier/add_supplier', supplier);
        return response.data;
    } catch (error) {
        console.error(error);
    }
};

const compareNames = (name1, name2) => {
    const name1Words = name1.toLowerCase().split(' ').sort();
    const name2Words = name2.toLowerCase().split(' ').sort();
    return JSON.stringify(name1Words) === JSON.stringify(name2Words);
};