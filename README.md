<img width="1219" alt="스크린샷 2024-05-02 201050" src="https://github.com/samhomesss/TeamProject/assets/159544864/fc0728a4-56a8-4bb9-b9a3-070a4c2561d3">
###[윤범 코드](https://github.com/samhomesss/TeamProject/tree/main/Assets/yb)
## **📃핵심 기술**

### ・상태 패턴을 활용한 체계적인 플레이어 행동 관리

🤔**WHY?**

플레이어의 행동을 플레이어 쪽에서 모아서 관리해 가시성이 떨어지고, 다른 팀원이 수정을 하려할 때 코드의 내용을 전체적으로 파악하고 있어야 해 효율이 크게 떨어짐

🤓**Result!**

플레이어의 각 상태에 맞는 행동을 분리해, 가시성이 높아지고 제3자의 수정, 추가, 제거가 원활해짐과 동시에 유지보수성이 크게 증가함

### ・플레이어와 관련된 모든 로직을 컴포지트 패턴을 이용해 분할

🤔**WHY?**

사운드 출력, 아이템 획득, 무기 등등 플레이어의 로직들을 플레이어 내부 코드에서 모두 처리해, 코드가 코드가 길어지고 가시성이 떨어지고, 다른 팀원의 코드 해석이 점점 힘들어짐

🤓**Result!**

플레이어에 관련된 모든 로직을 클래스별로 나누고 플레이어 쪽에서 선언하여 사용해, 가시성이 높아지고 클래스간의 결합도 하락 및 다른 팀원도 쉽게 수정이 가능

### ・옵저버 패턴을 이용한 UI 시스템

🤔**WHY?**

 데이터의 변경이 없음에도 주기적으로 UI에 데이터를 동기화해, 필요 없는 작업이 지속적으로 반복되어 결과적으로 퍼포먼스 하락

🤓**Result!**

게임이 시작될 때, 각 플레이어가 자신의 UI에 자신의 데이터를 연동해,, 실질적인 데이터의 변경이 발생할 때에만 UI에 데이터를 동기화해, 필요 없는 작업을 배제해 퍼포먼스 상승

### ・컴포지트 패턴과 전략 패턴을 이용한 추상화된 무기 관리 시스템

🤔**WHY?**

무기의 종류가 많아지고, 각 무기를 각각 구현해 중복된 코드를 작성하는 일이 잦아지고, 무기의 종류가 늘어나거나 줄어들 때 마다 직접적으로 플레이어 쪽 코드의 수정이 필요, 유지보수가  굉장히 힘든 문제가 발생

🤓**Result!**

무기의 특징들만을 추상화해, 플레이어는 추상화된 무기의 특징을 호출하는 것 만으로 실질적인 무기의 기능을 사용할 수 있게 되어, 무기에 변화가 생겨도 플레이어 쪽 코드의 수정이 불필요하게 되어 유지보수가 용이해짐.

### ・풀링 오브젝트 시스템

🤔**WHY?**

각종 오브젝트를 필요할 때 마다 생성, 필요가 없어지면 제거해 짧은 시간 내에 다량의 객체를 생성하는 상황이 발생 시 심각한 퍼포먼스 하락 발생

🤔**HOW?**

 


    
            
    
           
    ```
    

🤓**Result!**

  객체의 직접적인 생성 / 파괴를 최대한 피하고 풀링 시스템을 이용, 이미 생성된 객체를 재사용하는 과정을 통해 객체의 생성에 들어가는 비용을 줄여 퍼포먼스 상승
