// Смена цвета кнопки при наведении
const button = document.body.querySelectorAll(".buttonToChange");

for (let i = 0; i < button.length; i++) {
    button[i].addEventListener("mouseover", function () {
        button[i].classList.add("changebackground");
    });
    button[i].addEventListener("mouseout", function () {
        button[i].classList.remove("changebackground");
    });
}

// Модальное окно
const dialog = document.querySelector(".dialogMain");
const html = document.querySelector("html");
const openModels = document.querySelectorAll(".openModel");
const priceHeader = document.querySelector(".priceHeader");
let price;

for (let i = 0; i < openModels.length; i++) {
    openModels[i].addEventListener("click", function () {
        dialog.showModal();
        html.classList.add("changehtml");
        if (openModels[i].getAttribute("data-price") > 0) {
            price = parseInt(openModels[i].getAttribute("data-price"));
        }
        priceHeader.textContent = `Цена: ${price.toLocaleString()}$`;
    });
}

document.querySelector("#closeModel").onclick = function () {
    dialog.close();
    html.classList.remove("changehtml");
};

//Изменения цены
const formElement = document.querySelector(".orderForm");
const quantity = document.querySelector("#quantity");
const promo = document.querySelector("#promo");

const promoList = {
    "10procentSaleWOW": 0.9,
    "15yearShop": 0.85,
};

function updatePrice() {
    let quantityVal = 1;
    if (quantity.value > 1) {
        quantityVal = quantity.value;
    }
    let discount = promo.value in promoList ? promoList[promo.value] : 1;

    priceHeader.textContent = `Цена: ${createPrice(price, quantityVal, discount).toLocaleString()}$ (${quantityVal} шт, скидка ${Math.round((1 - discount) * 100)}%)`;
}

function createPrice(priceFunc, quantityFunc, discountFunc) {
    let finalPrice = priceFunc * quantityFunc * discountFunc;
    return finalPrice;
}

quantity.addEventListener("input", updatePrice);
promo.addEventListener("input", updatePrice);