export function GetConditionIcon(condition) {
    const normalizedCondition = condition.toLowerCase();
    const iconPath = `assets/icons/${normalizedCondition}.png`;

    // Define a default icon path if the specific icon is missing
    const defaultIconPath = 'assets/icons/sunny.png';

    // Return the image tag with fallback handling
    return `<img src="${iconPath}" onerror="this.src='${defaultIconPath}'" alt="${condition}" title="${condition}" style="width: 36px; height: 36px;">`;
}