#include <SFML/Graphics.hpp>
#include <vector>
#include <sstream>

using namespace sf;

const int WINDOW_WIDTH = 800;
const int WINDOW_HEIGHT = 600;
const int TOOLBAR_HEIGHT = 50;
const float MIN_BRUSH_RADIUS = 2.0f;
const float MAX_BRUSH_RADIUS = 20.0f;

enum ShapeType { Circle, Square, Triangle };

struct Brush {
    Color color;
    float radius;
    ShapeType shapeType = Circle; // Фигура по умолчанию — круг
};

class PaintApp {
private:
    RenderWindow window;
    RenderTexture canvas;
    Brush brush;
    Vector2f prevPosition;
    bool isDrawing = false;

    RectangleShape toolbar;
    std::vector<RectangleShape> colorButtons;
    std::vector<RectangleShape> sizeButtons;
    std::vector<RectangleShape> shapeButtons;
    std::vector<Color> colors = { Color::Black, Color::Red, Color::Green, Color::Blue, Color::Yellow, Color::White };
    std::vector<float> sizes = { 2.0f, 5.0f, 10.0f, 15.0f, 20.0f };
    Font font;
    std::vector<Text> sizeTexts;
    std::vector<Text> shapeTexts;

public:
    PaintApp() : window(VideoMode(WINDOW_WIDTH, WINDOW_HEIGHT), "Simple Paint") {
        canvas.create(WINDOW_WIDTH, WINDOW_HEIGHT);
        canvas.clear(Color::White);

        brush.color = Color::Black;
        brush.radius = 5.0f;

        if (!font.loadFromFile("arial.ttf")) {
            // Файл шрифта не найден
        }

        // Инициализация панели инструментов
        toolbar.setSize(Vector2f(WINDOW_WIDTH, TOOLBAR_HEIGHT));
        toolbar.setFillColor(Color(200, 200, 200));

        // Кнопки выбора цвета
        for (size_t i = 0; i < colors.size(); ++i) {
            RectangleShape colorButton(Vector2f(40, 40));
            colorButton.setFillColor(colors[i]);
            colorButton.setPosition(10 + i * 50, 5);
            colorButtons.push_back(colorButton);
        }

        // Кнопки выбора размера кисти
        for (size_t i = 0; i < sizes.size(); ++i) {
            RectangleShape sizeButton(Vector2f(40, 40));
            sizeButton.setFillColor(Color(150, 150, 150));
            sizeButton.setPosition(10 + (i + colors.size()) * 50, 5);
            sizeButtons.push_back(sizeButton);

            Text sizeText;
            sizeText.setFont(font);
            sizeText.setString(std::to_string(i + 1));
            sizeText.setCharacterSize(20);
            sizeText.setFillColor(Color::Black);
            sizeText.setPosition(sizeButton.getPosition().x + 12, sizeButton.getPosition().y + 8);
            sizeTexts.push_back(sizeText);
        }

        // Кнопки выбора фигуры
        std::vector<std::string> shapeLabels = { "Circle", "Square", "Triangle" };
        for (size_t i = 0; i < shapeLabels.size(); ++i) {
            RectangleShape shapeButton(Vector2f(80, 40));
            shapeButton.setFillColor(Color(200, 200, 200));
            shapeButton.setPosition(10 + (i + colors.size() + sizes.size()) * 50, 5);
            shapeButtons.push_back(shapeButton);

            Text shapeText;
            shapeText.setFont(font);
            shapeText.setString(shapeLabels[i]);
            shapeText.setCharacterSize(14);
            shapeText.setFillColor(Color::Black);
            shapeText.setPosition(shapeButton.getPosition().x + 5, shapeButton.getPosition().y + 10);
            shapeTexts.push_back(shapeText);
        }
    }

    void run() {
        while (window.isOpen()) {
            handleEvents();
            draw();
        }
    }

private:
    void handleEvents() {
        Event event;
        while (window.pollEvent(event)) {
            if (event.type == Event::Closed)
                window.close();

            // Обработка выбора цвета, размера кисти и фигуры
            if (event.type == Event::MouseButtonPressed && event.mouseButton.button == Mouse::Left) {
                Vector2i mousePos = Mouse::getPosition(window);
                if (mousePos.y < TOOLBAR_HEIGHT) {
                    for (size_t i = 0; i < colorButtons.size(); ++i) {
                        if (colorButtons[i].getGlobalBounds().contains(Vector2f(mousePos))) {
                            brush.color = colors[i];
                            return;
                        }
                    }
                    for (size_t i = 0; i < sizeButtons.size(); ++i) {
                        if (sizeButtons[i].getGlobalBounds().contains(Vector2f(mousePos))) {
                            brush.radius = sizes[i];
                            return;
                        }
                    }
                    for (size_t i = 0; i < shapeButtons.size(); ++i) {
                        if (shapeButtons[i].getGlobalBounds().contains(Vector2f(mousePos))) {
                            brush.shapeType = static_cast<ShapeType>(i);

                            // Устанавливаем максимальный размер кисти, если выбраны квадрат или треугольник
                            if (brush.shapeType == Square || brush.shapeType == Triangle) {
                                brush.radius = sizes.back(); // Максимальный размер
                            }
                            return;
                        }
                    }
                }
            }
        }

        // Рисование фигур
        if (Mouse::isButtonPressed(Mouse::Left) && Mouse::getPosition(window).y > TOOLBAR_HEIGHT) {
            Vector2f position = window.mapPixelToCoords(Mouse::getPosition(window));
            if (isDrawing) {
                drawShape(canvas, prevPosition, brush);
            }
            prevPosition = position;
            isDrawing = true;
        }
        else {
            isDrawing = false;
        }
    }

    void draw() {
        canvas.display();
        Sprite sprite(canvas.getTexture());

        window.clear(Color::White);
        window.draw(sprite);
        drawToolbar();
        window.display();
    }

    void drawToolbar() {
        window.draw(toolbar);
        for (auto& colorButton : colorButtons) {
            window.draw(colorButton);
        }
        for (size_t i = 0; i < sizeButtons.size(); ++i) {
            window.draw(sizeButtons[i]);
            window.draw(sizeTexts[i]);
        }
        for (size_t i = 0; i < shapeButtons.size(); ++i) {
            window.draw(shapeButtons[i]);
            window.draw(shapeTexts[i]);
        }
    }

    void drawShape(RenderTexture& canvas, Vector2f position, Brush brush) {
        if (brush.shapeType == Circle) {
            CircleShape circle(brush.radius);
            circle.setFillColor(brush.color);
            circle.setPosition(position - Vector2f(brush.radius, brush.radius));
            canvas.draw(circle);
        }
        else if (brush.shapeType == Square) {
            RectangleShape square(Vector2f(brush.radius * 2, brush.radius * 2));
            square.setFillColor(brush.color);
            square.setPosition(position - Vector2f(brush.radius, brush.radius));
            canvas.draw(square);
        }
        else if (brush.shapeType == Triangle) {
            ConvexShape triangle;
            triangle.setPointCount(3);
            triangle.setPoint(0, position + Vector2f(0, -brush.radius));
            triangle.setPoint(1, position + Vector2f(-brush.radius, brush.radius));
            triangle.setPoint(2, position + Vector2f(brush.radius, brush.radius));
            triangle.setFillColor(brush.color);
            canvas.draw(triangle);
        }
    }
};

int main() {
    PaintApp app;
    app.run();
    return 0;
}
