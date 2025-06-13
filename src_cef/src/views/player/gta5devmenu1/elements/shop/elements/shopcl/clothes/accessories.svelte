<script>
    import { accountRedbucks } from 'store/account'
    import { executeClient } from 'api/rage'

    export let SetPopup;
    export let searchText;

    let selectIndex = -1;

    let maxindex;

    let currentIndex = 0;

    const shopList = [
            {
                title: "BANZO",
                desc: "Ты шаришь за эту тему!",
                function: "onSelectC",
                img: "http://cdn.piecerp.ru/cloud/inventoryItems/clothes/male/accessories/338_0.png",
                price: 600000,
                line: "line4",
                list: [
                    {
                        id: 8,
                        name: "BANZO",
                        desc: "Ты шаришь за эту тему!",
                        text: `Одежда`,
                        img: "http://cdn.piecerp.ru/cloud/inventoryItems/clothes/male/accessories/338_0.png",
                        price: 600000,
                        line: "line4",
                    },
                    // {
                    //     id: 16,
                    //     name: "Яркий верх 2",
                    //     desc: "",
                    //     text: `Одежда`,
                    //     img: "https://imgur.com/Uq251dz.png",
                    //     price: 3000,
                    //     line: "line4",
                    // },
                    // {
                    //     id: 16,
                    //     name: "Яркий верх 3",
                    //     desc: "",
                    //     text: `Одежда`,
                    //     img: "https://imgur.com/Uq251dz.png",
                    //     price: 3000,
                    //     line: "line4",
                    // },
                    // {
                    //     id: 16,
                    //     name: "Яркий верх 4",
                    //     desc: "",
                    //     text: `Одежда`,
                    //     img: "https://imgur.com/Uq251dz.png",
                    //     price: 3000,
                    //     line: "line4",
                    // },
                ]
            },
            // {
            //     title: "Яркий низ",
            //     desc: "",
            //     function: "onSelectC",
            //     img: "https://imgur.com/Uq251dz.png",
            //     price: 3000,
            //     line: "line4",
            //     list: [
            //         {
            //             id: 16,
            //             name: "Яркий низ",
            //             desc: "",
            //             text: `Одежда`,
            //             img: "https://imgur.com/Uq251dz.png",
            //             price: 3000,
            //             line: "line4",
            //         },
            //         {
            //             id: 16,
            //             name: "Яркий низ 2",
            //             desc: "",
            //             text: `Одежда`,
            //             img: "https://imgur.com/Uq251dz.png",
            //             price: 3000,
            //             line: "line4",
            //         },
            //         {
            //             id: 16,
            //             name: "Яркий низ 3",
            //             desc: "",
            //             text: `Одежда`,
            //             img: "https://imgur.com/Uq251dz.png",
            //             price: 3000,
            //             line: "line4",
            //         },
            //         {
            //             id: 16,
            //             name: "Яркий низ 4",
            //             desc: "",
            //             text: `Одежда`,
            //             img: "https://imgur.com/Uq251dz.png",
            //             price: 3000,
            //             line: "line4",
            //         },
            //     ]
            // },
            // {
            //     title: "Яркий хуй",
            //     desc: "",
            //     function: "onSelectC",
            //     img: "https://imgur.com/Uq251dz.png",
            //     price: 3000,
            //     line: "line4",
            //     list: [
            //         {
            //             id: 16,
            //             name: "Яркий хуй",
            //             desc: "",
            //             text: `Одежда`,
            //             img: "https://imgur.com/Uq251dz.png",
            //             price: 3000,
            //             line: "line4",
            //         },
            //         {
            //             id: 16,
            //             name: "Яркий хуй 2",
            //             desc: "",
            //             text: `Одежда`,
            //             img: "https://imgur.com/Uq251dz.png",
            //             price: 3000,
            //             line: "line4",
            //         },
            //         {
            //             id: 16,
            //             name: "Яркий хуй 3",
            //             desc: "",
            //             text: `Одежда`,
            //             img: "https://imgur.com/Uq251dz.png",
            //             price: 3000,
            //             line: "line4",
            //         },
            //         {
            //             id: 16,
            //             name: "Яркий хуй 4",
            //             desc: "",
            //             text: `Одежда`,
            //             img: "https://imgur.com/Uq251dz.png",
            //             price: 3000,
            //             line: "line4",
            //         },
            //     ]
            // }
    ];

    function shuffle(array) {
    for (let i = array.length - 1; i > 0; i--) {
      let j = Math.floor(Math.random() * (i + 1));
      [array[i], array[j]] = [array[j], array[i]];
    }
    return array;
    }

    let shuffledList = shuffle(shopList);
    let randomItems = shuffledList.slice(0, 4);

    const selected = (index, title) => {
        selectIndex = index;
        currentIndex = 0;
        maxindex = shopList[index].list.length - 1;
        switchBlock();
        window.pageloadf2 (3);
        window.pagenameitemf2 (title);
    }

    function switchBlock() {
    const blocks = document.querySelectorAll('.blgf');
    blocks.forEach((block, index) => {
        if (index === currentIndex) {
        block.style.display = 'flex';
        } else {
        block.style.display = 'none';
        }
    });
    }

    const nextBlock = () => {
        currentIndex = (currentIndex + 1);
        switchBlock();
    }

    const prevBlock = () => {
        currentIndex = (currentIndex - 1);
        switchBlock();
    }

    const onBuy = (item) => {
        if ($accountRedbucks < item.price)
            return window.notificationAdd(4, 9, `Недостаточно AstraCoin!`, 3000);
        SetPopup ()
        executeClient ("client.donate.buy.clothes", item.id);
        window.notificationAdd(3, 9, `Поздравляем с покупкой`, 3000);
    }

</script>
{#if selectIndex > -1}
    <div class="selectblockcl">
        {#each shopList[selectIndex].list as item, index}
        <div class="blgf" class:act={index === 0}>
            <div class="headsbcl">
                <h1>{item.name}</h1>
                <span>{item.price}
                    <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink">
                        <rect width="24" height="24" fill="url(#pattern0_1143_78)"/>
                        <defs>
                        <pattern id="pattern0_1143_78" patternContentUnits="objectBoundingBox" width="1" height="1">
                        <use xlink:href="#image0_1143_78" transform="scale(0.0416667)"/>
                        </pattern>
                        <image id="image0_1143_78" width="24" height="24" xlink:href="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAACXBIWXMAAAsTAAALEwEAmpwYAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAARySURBVHgBrVUPTJRlGP+9333fffcHTg64A1RAERIwFUJpMq2hENBaAxe5XKEs51o53XLW5paItdyq1Vi6mtnYcG1MrJUrSA1xmWvhdEwDoUaEB4LHcYfeccfdffe9vd93gEBIM/ht3z3PPc/zPc/7/p7nfT+COdAYX2kxuQZeDlEUcLK8joBamJlQwjtDvOaGXxd1tSe14Pu2nB3dggC33w/fiROQlJCJHGS2xC0Ju2Ph6v+IDwW3EUp1+A94TAnnOlaXHbc9UXq9rw/OzEz4q6uJPGuBZn1ZuRga/QQyXYJHQEgjePoTc49dKar6PBjEUEICxpQi0wo0Rzy/RxzzfYp5YMiS8dn50pqjDgccyk40E46fzBVrRN/IN6wih3nA6HWsj/D093uyNnbpdAioBc7FvWI13rvbDEqjsQBY5OrNc2ljL9jEVIe6WvHeyGuU0mUzAwlHQHhOfeYCbzLAsCYFxpw0cDot2MRFZLV/vZu5DIwRSi4LJXZCQ7EzX8xu/xK61HCvfy84APflm9P84rJ4LK95A1H5WSCioNpoUIKzsRVdL70XvLLpYA53SVtSOlvyuF3PTiZXYMhInuaP3JDJFnAS5uL18A86Ya+7AOcPv4EIPAzpSSAhWci6frKM11C6ZWZyMTkOSUd2qrrkcoM3R0L/+LIH/qQ4rDx9iFHHq4n/2nsM1OdXfbqMRFi2F4R1rzOdY9wvn1kg8eB28DEmDP/YCkf9pXBwcvyk3/pqMQRrFAL2EfS9e2oyuYKxWzbY3qlVdSLLqayNWDRz65adRWygKHr3Hoevy6batUtiJmNMeatU6b3ZA/9tOx4GDZWsHDfjNKfVHlDl6NUuGNamMDqs47QxScKhYqJFlUG7C3NBBgmyGcTtCUPCnlKIyxNUPSI3HekNVVj85gvqf95khHZx+JiEfAFVTqVtIiaqJHfKDjDAsxvJSRBu7JK3tqmOvg/q4fn1lqoTDYeVZ6pU3ZCdhkD/MO63tMGQmYzIvEys+GI/PK2d6sRZGbX22iaMNLWGF0LQy4eocIZH4HUluRBnhr93ELbqOjY+8oOlsX4o9AhsmhTYjpyCPiMJizZnw1pRqD4KJJcHQ19dnHyNyPS8ctC4X7TFdsOq5BilHcG7TsbtyLStG1anqDIw4IDkuD9pN65dATFlMTSRenjbe+DrtEEeHRvPrvHeSXvSTKqqKJf/YWkVH/AewgIiRISPnw427ueqq4HvNr1dI3FCFxYIjNG/h3Wx7yu6cotR6bENo90Zz+wKcYIb8wXhvF69fsdWd93weAFCBwchtT2174Z96dp9lHBB/F8QQgNiRGWR5+zPEyb1e9DRcRhZWZA7l27pNtLRi9GOPzcTKpseITUouDuj+ujnCj0NTVPt41+0aly7dphGRUEaW7NuwG2IOW0Z/sOvkYIZBLJhzsyMEvZzVNJHVxa66zv/5Z5lLaS8HJzZDC4QgKai4cWtvOTLJ5BS2eUVz2ablzlNH5uTnoDW2CLq5W83Os4+tHf/AMnopNuj/8OFAAAAAElFTkSuQmCC"/>
                        </defs>
                    </svg>                                 
                </span>
                <div class="btnbuy" on:keypress on:click={() => onBuy (item)}>Купить предмет</div>
            </div>
            <div class="bgblocksbcl">
                <div class="linesbcl {item.line}"></div>
                <div class="centersbcl">
                    {#if currentIndex <= 0}
                        <span class="rligh" class:none={currentIndex <= 0}>
                            <svg width="30" height="30" viewBox="0 0 30 30" fill="none" xmlns="http://www.w3.org/2000/svg">
                                <rect x="30" y="30" width="30" height="30" rx="15" transform="rotate(-180 30 30)" fill="#262626"/>
                                <path d="M18 10L11.4 15.5L18 21" stroke="white" stroke-opacity="0.5" stroke-linecap="round"/>
                            </svg> 
                        </span>
                        {:else}
                        <span class="rligh" class:none={currentIndex <= 0} on:keypress on:click={() => prevBlock ()}>
                            <svg width="30" height="30" viewBox="0 0 30 30" fill="none" xmlns="http://www.w3.org/2000/svg">
                                <rect x="30" y="30" width="30" height="30" rx="15" transform="rotate(-180 30 30)" fill="#262626"/>
                                <path d="M18 10L11.4 15.5L18 21" stroke="white" stroke-opacity="0.5" stroke-linecap="round"/>
                            </svg> 
                        </span>
                    {/if}
                    <img src="{item.img}" alt=""/>
                    {#if currentIndex === maxindex}
                        <span class="rligh" class:none={currentIndex === maxindex}>
                            <svg width="30" height="30" viewBox="0 0 30 30" fill="none" xmlns="http://www.w3.org/2000/svg">
                                <rect width="30" height="30" rx="15" fill="#262626"/>
                                <path d="M12 20L18.6 14.5L12 9" stroke="white" stroke-opacity="0.5" stroke-linecap="round"/>
                            </svg>                                                                              
                        </span>
                        {:else}
                        <span class="rligh" class:none={currentIndex === maxindex} on:keypress on:click={() => nextBlock ()}>
                            <svg width="30" height="30" viewBox="0 0 30 30" fill="none" xmlns="http://www.w3.org/2000/svg">
                                <rect width="30" height="30" rx="15" fill="#262626"/>
                                <path d="M12 20L18.6 14.5L12 9" stroke="white" stroke-opacity="0.5" stroke-linecap="round"/>
                            </svg>                                                                              
                        </span>
                    {/if}
                </div>
            </div>
        </div>
        {/each}
        <div class="catalogitems">
            <h1>Другие модели</h1>
            <div class="clilist">
                {#each randomItems as item, index} 
                    <div class="clotblock" on:keypress on:click={() => selected (index, item.title)}>
                        <div class="headcbl">
                            <div class="linesbcl {item.line}"></div>
                            <span>{item.price}
                                <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink">
                                    <rect width="24" height="24" fill="url(#pattern0_1143_78)"/>
                                    <defs>
                                    <pattern id="pattern0_1143_78" patternContentUnits="objectBoundingBox" width="1" height="1">
                                    <use xlink:href="#image0_1143_78" transform="scale(0.0416667)"/>
                                    </pattern>
                                    <image id="image0_1143_78" width="24" height="24" xlink:href="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAACXBIWXMAAAsTAAALEwEAmpwYAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAARySURBVHgBrVUPTJRlGP+9333fffcHTg64A1RAERIwFUJpMq2hENBaAxe5XKEs51o53XLW5paItdyq1Vi6mtnYcG1MrJUrSA1xmWvhdEwDoUaEB4LHcYfeccfdffe9vd93gEBIM/ht3z3PPc/zPc/7/p7nfT+COdAYX2kxuQZeDlEUcLK8joBamJlQwjtDvOaGXxd1tSe14Pu2nB3dggC33w/fiROQlJCJHGS2xC0Ju2Ph6v+IDwW3EUp1+A94TAnnOlaXHbc9UXq9rw/OzEz4q6uJPGuBZn1ZuRga/QQyXYJHQEgjePoTc49dKar6PBjEUEICxpQi0wo0Rzy/RxzzfYp5YMiS8dn50pqjDgccyk40E46fzBVrRN/IN6wih3nA6HWsj/D093uyNnbpdAioBc7FvWI13rvbDEqjsQBY5OrNc2ljL9jEVIe6WvHeyGuU0mUzAwlHQHhOfeYCbzLAsCYFxpw0cDot2MRFZLV/vZu5DIwRSi4LJXZCQ7EzX8xu/xK61HCvfy84APflm9P84rJ4LK95A1H5WSCioNpoUIKzsRVdL70XvLLpYA53SVtSOlvyuF3PTiZXYMhInuaP3JDJFnAS5uL18A86Ya+7AOcPv4EIPAzpSSAhWci6frKM11C6ZWZyMTkOSUd2qrrkcoM3R0L/+LIH/qQ4rDx9iFHHq4n/2nsM1OdXfbqMRFi2F4R1rzOdY9wvn1kg8eB28DEmDP/YCkf9pXBwcvyk3/pqMQRrFAL2EfS9e2oyuYKxWzbY3qlVdSLLqayNWDRz65adRWygKHr3Hoevy6batUtiJmNMeatU6b3ZA/9tOx4GDZWsHDfjNKfVHlDl6NUuGNamMDqs47QxScKhYqJFlUG7C3NBBgmyGcTtCUPCnlKIyxNUPSI3HekNVVj85gvqf95khHZx+JiEfAFVTqVtIiaqJHfKDjDAsxvJSRBu7JK3tqmOvg/q4fn1lqoTDYeVZ6pU3ZCdhkD/MO63tMGQmYzIvEys+GI/PK2d6sRZGbX22iaMNLWGF0LQy4eocIZH4HUluRBnhr93ELbqOjY+8oOlsX4o9AhsmhTYjpyCPiMJizZnw1pRqD4KJJcHQ19dnHyNyPS8ctC4X7TFdsOq5BilHcG7TsbtyLStG1anqDIw4IDkuD9pN65dATFlMTSRenjbe+DrtEEeHRvPrvHeSXvSTKqqKJf/YWkVH/AewgIiRISPnw427ueqq4HvNr1dI3FCFxYIjNG/h3Wx7yu6cotR6bENo90Zz+wKcYIb8wXhvF69fsdWd93weAFCBwchtT2174Z96dp9lHBB/F8QQgNiRGWR5+zPEyb1e9DRcRhZWZA7l27pNtLRi9GOPzcTKpseITUouDuj+ujnCj0NTVPt41+0aly7dphGRUEaW7NuwG2IOW0Z/sOvkYIZBLJhzsyMEvZzVNJHVxa66zv/5Z5lLaS8HJzZDC4QgKai4cWtvOTLJ5BS2eUVz2ablzlNH5uTnoDW2CLq5W83Os4+tHf/AMnopNuj/8OFAAAAAElFTkSuQmCC"/>
                                    </defs>
                                </svg>                                   
                            </span>
                        </div>
                        <img src="{item.img}" alt=""/>
                        <p>{item.title}</p>
                    </div>
                {/each}
            </div>
        </div>
    </div>
    {:else}
    <h1 class="shop">Аксессуары</h1>
    <div class="list1">
        {#each shopList as item, index}
            {#if (!searchText || !searchText.length) || (searchText && item.title.toLowerCase().trim().includes(searchText.toLowerCase().trim()))}     
                <div class="blockrewrad" on:keypress on:click={() => selected (index, item.title)}>
                    <div class="headrewrad">
                        <div class="line {item.line}"></div>
                        <span>{item.price}
                            <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink">
                                <rect width="24" height="24" fill="url(#pattern0_1143_78)"/>
                                <defs>
                                <pattern id="pattern0_1143_78" patternContentUnits="objectBoundingBox" width="1" height="1">
                                <use xlink:href="#image0_1143_78" transform="scale(0.0416667)"/>
                                </pattern>
                                <image id="image0_1143_78" width="24" height="24" xlink:href="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAACXBIWXMAAAsTAAALEwEAmpwYAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAARySURBVHgBrVUPTJRlGP+9333fffcHTg64A1RAERIwFUJpMq2hENBaAxe5XKEs51o53XLW5paItdyq1Vi6mtnYcG1MrJUrSA1xmWvhdEwDoUaEB4LHcYfeccfdffe9vd93gEBIM/ht3z3PPc/zPc/7/p7nfT+COdAYX2kxuQZeDlEUcLK8joBamJlQwjtDvOaGXxd1tSe14Pu2nB3dggC33w/fiROQlJCJHGS2xC0Ju2Ph6v+IDwW3EUp1+A94TAnnOlaXHbc9UXq9rw/OzEz4q6uJPGuBZn1ZuRga/QQyXYJHQEgjePoTc49dKar6PBjEUEICxpQi0wo0Rzy/RxzzfYp5YMiS8dn50pqjDgccyk40E46fzBVrRN/IN6wih3nA6HWsj/D093uyNnbpdAioBc7FvWI13rvbDEqjsQBY5OrNc2ljL9jEVIe6WvHeyGuU0mUzAwlHQHhOfeYCbzLAsCYFxpw0cDot2MRFZLV/vZu5DIwRSi4LJXZCQ7EzX8xu/xK61HCvfy84APflm9P84rJ4LK95A1H5WSCioNpoUIKzsRVdL70XvLLpYA53SVtSOlvyuF3PTiZXYMhInuaP3JDJFnAS5uL18A86Ya+7AOcPv4EIPAzpSSAhWci6frKM11C6ZWZyMTkOSUd2qrrkcoM3R0L/+LIH/qQ4rDx9iFHHq4n/2nsM1OdXfbqMRFi2F4R1rzOdY9wvn1kg8eB28DEmDP/YCkf9pXBwcvyk3/pqMQRrFAL2EfS9e2oyuYKxWzbY3qlVdSLLqayNWDRz65adRWygKHr3Hoevy6batUtiJmNMeatU6b3ZA/9tOx4GDZWsHDfjNKfVHlDl6NUuGNamMDqs47QxScKhYqJFlUG7C3NBBgmyGcTtCUPCnlKIyxNUPSI3HekNVVj85gvqf95khHZx+JiEfAFVTqVtIiaqJHfKDjDAsxvJSRBu7JK3tqmOvg/q4fn1lqoTDYeVZ6pU3ZCdhkD/MO63tMGQmYzIvEys+GI/PK2d6sRZGbX22iaMNLWGF0LQy4eocIZH4HUluRBnhr93ELbqOjY+8oOlsX4o9AhsmhTYjpyCPiMJizZnw1pRqD4KJJcHQ19dnHyNyPS8ctC4X7TFdsOq5BilHcG7TsbtyLStG1anqDIw4IDkuD9pN65dATFlMTSRenjbe+DrtEEeHRvPrvHeSXvSTKqqKJf/YWkVH/AewgIiRISPnw427ueqq4HvNr1dI3FCFxYIjNG/h3Wx7yu6cotR6bENo90Zz+wKcYIb8wXhvF69fsdWd93weAFCBwchtT2174Z96dp9lHBB/F8QQgNiRGWR5+zPEyb1e9DRcRhZWZA7l27pNtLRi9GOPzcTKpseITUouDuj+ujnCj0NTVPt41+0aly7dphGRUEaW7NuwG2IOW0Z/sOvkYIZBLJhzsyMEvZzVNJHVxa66zv/5Z5lLaS8HJzZDC4QgKai4cWtvOTLJ5BS2eUVz2ablzlNH5uTnoDW2CLq5W83Os4+tHf/AMnopNuj/8OFAAAAAElFTkSuQmCC"/>
                                </defs>
                            </svg>                            
                        </span>
                    </div>
                    <img class="n" src="{item.img}" alt=""/>
                    <p>{item.title}</p>
                </div>
            {/if}
        {/each}
    </div>
{/if}