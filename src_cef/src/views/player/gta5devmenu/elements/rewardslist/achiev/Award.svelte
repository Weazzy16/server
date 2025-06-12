<script>
  /**
   * Award — компонент для отображения награды в списке ачивок
   * Props:
   *  - award: Object
   *      • award.type (string) — тип награды ("item", "case", "donate")
   *      • award.color (string) — редкость ("red", "gold", "pink", "purple", "blue", "gray", "unique")
   *      • award.imagePath (string) — путь к иконке
   *      • award.title (string) — название награды
   */
  export let award;
  const { type, color, imagePath, title } = award || {};
</script>

<div class="award row-block align-center justify-start">
  <div class="award-card">
    <!-- Rarity stripe with radial gradient inside -->
    <div class={`award-card__rarity award-card__rarity-${color}`}>
      <svg fill="none" viewBox="0 0 16 4" xmlns="http://www.w3.org/2000/svg">
        <path
          d="M14 0H2C0.89543 0 0 0.89543 0 2C0 3.10457 0.89543 4 2 4H14C15.1046 4 16 3.10457 16 2C16 0.89543 15.1046 0 14 0Z"
          fill="var(--rarity-color)"
        />
      </svg>
      <!-- Radial gradient overlay restricted to stripe -->
      <div class="award-card__rarity__radial-gradient"></div>
    </div>
    <!-- Item image -->
    <img
      src={imagePath}
      alt={title}
      class={`award-card__media ${type}`}
    />
  </div>

  <div class="award--info column-block align-start justify-start full-width">
    <span class="award--info--type innerElem">{type}</span>
    <span class="award--info--title innerElem">{title}</span>
  </div>
</div>
