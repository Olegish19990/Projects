#include <SFML/Graphics.hpp>
#include <vector>
#include <sstream>

using namespace sf;

// ���������
const int WINDOW_WIDTH = 800;
const int WINDOW_HEIGHT = 600;
const int TOOLBAR_HEIGHT = 50;
const float MIN_BRUSH_RADIUS = 2.0f;
const float MAX_BRUSH_RADIUS = 20.0f;

// ��������� �����
struct Brush {
    Color color;
    float radius;
};

// ����� PaintApp ��� ����������� ����
class PaintApp {
private:
    RenderWindow window;
    RenderTexture canvas;
    Brush brush;
    Vector2f prevPosition;
    bool isDrawing = false;

    // ������ ������������
    RectangleShape toolbar;
    std::vector<RectangleShape> colorButtons;
    std::vector<RectangleShape> sizeButtons;
    std::vector<Color> colors = { Color::Black, Color::Red, Color::Green, Color::Blue, Color::Yellow, Color::White };
    std::vector<float> sizes = { 2.0f, 5.0f, 10.0f, 15.0f, 20.0f }; // ����������������� ������� �����
    Font font; // ����� ��� ������
    std::vector<Text> sizeTexts; // ������ ��� ������ �������

public:
    PaintApp() : window(VideoMode(WINDOW_WIDTH, WINDOW_HEIGHT), "Simple Paint") {
        canvas.create(WINDOW_WIDTH, WINDOW_HEIGHT);
        canvas.clear(Color::White);

        // ������������� �����
        brush.color = Color::Black;
        brush.radius = 5.0f;

        // �������� ������
        if (!font.loadFromFile("arial.ttf")) { // ���������, ��� ���� ������ ��������
            // ��������� ������
        }

        // ������������� ������ ������������
        toolbar.setSize(Vector2f(WINDOW_WIDTH, TOOLBAR_HEIGHT));
        toolbar.setFillColor(Color(200, 200, 200));

        // �������� ������ ��� ������ �����
        for (size_t i = 0; i < colors.size(); ++i) {
            RectangleShape colorButton(Vector2f(40, 40));
            colorButton.setFillColor(colors[i]);
            colorButton.setPosition(10 + i * 50, 5);
            colorButtons.push_back(colorButton);
        }

        // �������� ������ ��� ������ ������� �����
        for (size_t i = 0; i < sizes.size(); ++i) {
            RectangleShape sizeButton(Vector2f(40, 40));
            sizeButton.setFillColor(Color(150, 150, 150)); // ���� ������ �������
            sizeButton.setPosition(10 + (i + colors.size()) * 50, 5);
            sizeButtons.push_back(sizeButton);

            // �������� ������ ��� ������ �������
            Text sizeText;
            sizeText.setFont(font);
            sizeText.setString(std::to_string(i + 1)); // ����� �� 1 �� 5
            sizeText.setCharacterSize(20); // ������ ������
            sizeText.setFillColor(Color::Black); // ���� ������
            sizeText.setPosition(sizeButton.getPosition().x + 12, sizeButton.getPosition().y + 8); // ������������� ������
            sizeTexts.push_back(sizeText);
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

            // ��������� ������ �����
            if (event.type == Event::MouseButtonPressed && event.mouseButton.button == Mouse::Left) {
                Vector2i mousePos = Mouse::getPosition(window);
                if (mousePos.y < TOOLBAR_HEIGHT) { // ���� ���� � �������� ������ ������������
                    // �������� ������ �����
                    for (size_t i = 0; i < colorButtons.size(); ++i) {
                        if (colorButtons[i].getGlobalBounds().contains(Vector2f(mousePos))) {
                            brush.color = colors[i];
                            return;
                        }
                    }

                    // �������� ������ �������
                    for (size_t i = 0; i < sizeButtons.size(); ++i) {
                        if (sizeButtons[i].getGlobalBounds().contains(Vector2f(mousePos))) {
                            brush.radius = sizes[i]; // ��������� ������� �����
                            return;
                        }
                    }
                }
            }
        }

        // ��������� �����
        if (Mouse::isButtonPressed(Mouse::Left) && Mouse::getPosition(window).y > TOOLBAR_HEIGHT) {
            Vector2f position = window.mapPixelToCoords(Mouse::getPosition(window));
            if (isDrawing) {
                drawCircle(canvas, prevPosition, brush);
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
            window.draw(sizeTexts[i]); // ��������� ������ �� ������� �������
        }
    }

    // ������� ��� ��������� ����� (��� �����)
    void drawCircle(RenderTexture& canvas, Vector2f position, Brush brush) {
        CircleShape circle(brush.radius);
        circle.setFillColor(brush.color);
        circle.setPosition(position - Vector2f(brush.radius, brush.radius));
        canvas.draw(circle);
    }
};

int main() {
    PaintApp app;
    app.run();
    return 0;
}
