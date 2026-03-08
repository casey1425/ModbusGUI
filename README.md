# 📡 Modbus TCP/RTU Multi-Monitoring Tool
C# WinForms를 활용한 Modbus 통신(TCP / Serial RTU) 및 실시간 다중 모니터링 클라이언트 프로그램입니다.

## 📝 프로젝트 개요
산업용 표준 통신 프로토콜인 Modbus(TCP 및 Serial RTU)를 지원하는 GUI 기반의 클라이언트 툴입니다. 기존의 단일 창 모니터링 방식에서 벗어나, 여러 개의 Function Code와 주소 대역을 동시에 실시간으로 감시할 수 있는 **다중 모니터링(MDI) 기능**에 초점을 맞추어 개발했습니다.

## 🚀 주요 기능 (Key Features)
* **다중 통신 프로토콜 지원**: 
  * `Modbus TCP` (IP/Port 기반 통신)
  * `Modbus RTU` (Serial COM Port 기반 통신)
* **독립적인 멀티 모니터링 창**: 각기 다른 Function Code(Coil, Input, Holding Register 등)와 주소 범위를 가진 모니터링 창을 여러 개 띄워 동시에 감시할 수 있습니다.
* **실시간 자동 갱신 (Auto Refresh)**: `Timer`를 활용하여 500ms 주기로 데이터를 자동 폴링(Polling)합니다.
* **비동기 처리 (Async/Await)**: UI 스레드 차단(Freezing) 없이 부드러운 화면 전환과 통신을 보장합니다.

## 🛠 기술 스택 (Tech Stack)
* **Language**: C# 
* **Framework**: .NET [버전 입력, 예: Framework 4.7.2 또는 .NET 6.0]
* **UI**: Windows Forms (WinForms)
* **Architecture**: OOP 기반 Transport 인터페이스 분리 (`IModbusTransport`)

## ⚙️ 아키텍처 및 핵심 구현 (Architecture)

### 1. 객체지향적 통신 설계 (Interface Segregation)
통신 방식(TCP/Serial)에 종속되지 않도록 `IModbusTransport` 인터페이스를 구현하여 다형성(Polymorphism)을 확보했습니다. 이를 통해 `ModbusClient`는 하위 통신 방식의 변화에 영향을 받지 않습니다.

### 2. 계층별 에러 핸들링 구조 (Layered Error Handling)
통신 중 발생할 수 있는 다양한 예외를 3단계로 나누어 방어적으로 처리합니다.
1. **Transport Layer**: Timeout, Disconnect 등 물리적 연결 예외 처리
2. **Protocol Layer**: Length, CRC 검증 및 Modbus Exception Code (0x80 이상) 처리
3. **Application Layer**: 예외를 직접 던지지 않고 `ModbusResult` 객체로 래핑(Wrapping)하여 UI 레이어의 부담을 최소화.

### 3. 타이머 기반 중복 요청 방지 (Busy Flag)
```csharp
if (_isBusy) return; // 이전 통신이 끝나지 않았으면 Skip
_isBusy = true;
// ... 통신 로직 ...
_isBusy = false;
