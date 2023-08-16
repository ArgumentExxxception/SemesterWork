const crypto =`<div class="css-1had40c">
    <img style="width: 5%" src="" class="crypto_img" alt=""/>
    <div class="name"></div>
    <div class="price"></div>
    <div class="priceChange"></div>
    <div class="total_volume"></div>
    <a href="" class="trade">Торговать</a>
</div>`

const mapToLine = (param) =>{
    const elem = document.createElement("div");
    elem.innerHTML = crypto
    $(".crypto_img",elem).first().attr("src",param.image)
    $(".name",elem).first().text(param.name)
    $(".price",elem).first().text(`${param.current_price} $`);
    $(".priceChange",elem).first().text(`${param.price_change_percentage_24h} %`)
    $(".total_volume",elem).first().text(`${param.total_volume} $`);
    $(".trade",elem).first().attr("href",`/Market/${param.id}`)
    return elem
}
$(document).ready(()=>{
    $.ajax({
        url: '/priceCrypto',
        type: 'GET',
        contentType: 'application/json',
        success: function (data) {
            const crypto = data.map(x=>mapToLine(x));
            $("#content").empty().append(crypto)
        }
    })
})