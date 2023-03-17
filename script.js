const animateItems = document.querySelectorAll('.animate');

const observer = new IntersectionObserver(entries => {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            entry.target.classList.add('animate__animated', 'animate__fadeInUp');
            observer.unobserve(entry.target);
        }
    });
}, {
    rootMargin: '0% 0% -90% 0%'
});

animateItems.forEach(item => {
    observer.observe(item);
});

/* Commented out Mars-related code
const cat = document.createElement("img");
cat.src = "img/MarsOnMoon.png";
cat.style.position = "fixed";
cat.style.width = "100px";
cat.style.top = "70px"; // adjust as needed
cat.style.right = "20px"; // adjust as needed
document.body.appendChild(cat);

const catSound = new Audio("audio/362652__trngle__cat-meow.wav"); // create an Audio object

cat.addEventListener("click", () => {
    cat.classList.toggle("hidden");
    catSound.play(); // play the cat sound when the user clicks on the cat image
});

let direction = 1;
let position = -100;

function animateCat() {
    ...
}

function toggleCat() {
    cat.classList.toggle("hidden");
}

function changeCatColor() {
    ...
}

setInterval(() => {
    animateCat();
}, 10);

setInterval(() => {
    toggleCat();
}, 2000);

setInterval(() => {
    changeCatColor();
}, 3000);
*/

const title = document.querySelector('.home-content h1');

title.addEventListener('mouseover', () => {
    title.style.animation = 'wiggle 0.5s ease-in-out';
});

title.addEventListener('animationend', () => {
    title.style.animation = '';
});

const menuIcon = document.querySelector('.menu-icon');
const navMenu = document.querySelector('.nav-menu');

menuIcon.addEventListener('click', () => {
    navMenu.classList.toggle('active');
});

/* social links highlighting */
const contactLink = document.querySelector('#contact a');
const socialLinks = document.querySelectorAll('.social-links a');

contactLink.addEventListener('click', (event) => {
    event.preventDefault(); // prevent default scrolling behavior

    socialLinks.forEach((link) => {
        link.classList.add('highlight-icon', 'grow-icon');

        // remove highlight and grow classes after animation completes
        setTimeout(() => {
            link.classList.remove('highlight-icon', 'grow-icon');
        }, 1000);
    });
});