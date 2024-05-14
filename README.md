<img width="1219" alt="스크린샷 2024-05-02 201050" src="https://github.com/samhomesss/TeamProject/assets/159544864/fc0728a4-56a8-4bb9-b9a3-070a4c2561d3">


  

  ## **목차**
  

- [프로젝트 소개](#프로젝트-소개)
- [팀원 소개](#팀원-소개)
- [채택한 개발 기술과 브랜치 전략](#채택한-개발-기술과-브랜치-전략)
- [역할 분담](#역할-분담)
- [개발 기간 및 작업 관리](#개발-기간-및-작업-관리)
- [보안점](#보안점) 


## **프로젝트 소개** 
```scala
- Casual Battle은 가장 많은 땅을 가진 플레이어가 우승을 하는 게임입니다
- 내가 원하는 총과 유물 2가지를 조합하여 나만의 배틀스타일을 꾸밀 수 있습니다.
```

## **팀원 소개** 

|  **이희웅**  |  **오윤범**  |  **윤승현**  |
|  ------  |  ------  |  ------  |
| [**@heewoung-lee**](https://github.com/heewoung-lee)  |  [**@oyb1412**](https://github.com/oyb1412) |  [**@Yoon.07.09**](https://github.com/samhomesss)  |

## **채택한 개발 기술과 브랜치 전략** 
```scala
브랜치 전략과 채택한 개발 기술

・브랜치 전략
  Github-flow 전략을 기반으로 main브랜치 및 Topic 보조 브랜치를 운용하였습니다.
  단 상세한 기능 하나하나를 Topic 브랜치로 모두 나눈 것이 아닌,
  서로의 분담된 담당 역할(서버, UI, 로직)을 기준으로 한 번만 Topic를 나눠 작업을 진행했습니다.
  또한 아직 완성되지 않은 기능이더라도 꾸준한 커밋을 통해 코드 보관 및 서로간의 자체
  피드백을 통해 프로젝트를 완성해나갈 수 있었습니다.

- main 브랜치는 배포 단계 및 개발 단계에서 master역할을 하는 브랜치입니다.
  언제나 Stable한 상태를 유지하도록 했습니다.

- Topic 브랜치는 각자 담당하고 있는 역할 단위로, 서로간의 독립적인 개발 환경을 위해
  사용했습니다.

・개발 기술

- Firebase 및 Photon 라이브러리를 이용해 웹 서버 및 실시간 네트워크 환경을 구축했습니다.

- 적극적인 객체의 추상화를 통해, 손쉽게 클래스를 공유해 사용할 수 있게 했고 유지보수성을 향상시켰습니다.



```


## **역할 분담** 
**🍊이희웅**

- **DB(FireBase)**
    - 로그인
    - 회원가입
- **NetWork(Photon Pun2)**
    - 매치메이킹,동기화 전반
##
**🍊오윤범**

-  메인 로직(동적 객체)
-  플레이어 로직
-  아이템 로직
-  발사체 로직 및 타 객체와의 상호작용
-  파괴 가능하거나 불가능한 객체와의 상호작용
-  아이템 습득 및 사용
##
**🍊윤승현** 
- 메인 UI 제작
- 플레이어 인벤토리 로직
- 플레이어와 Map간 상호작용 판정 로직
- 아이템 정보창
- 미니맵 + 현재 플레이어들이 취득한 땅의 비율
- 아이템 드롭 로직 구현
- 게임 엔딩 씬 구현

## **개발 기간 및 작업 관리** 

**개발 기간**

- 전체 개발 기간 : 2024-03-29 ~ 2024-04-22
- 기획 :  2024-03-29 ~ 2024-04-01
- 프로토타입 개발 : 2024-04-02 ~ 2024-04-12
- 디버깅 및 폴리싱 : 2024-04-13 ~ 2024-04-22
##
**작업 관리**

- GitHub를 통한 협업

## **보안점** 

- [희웅 보완점](#희웅-보완점)
- [윤범 보완점](#윤범-보완점)
- [승현 보완점](#승현-보완점)
##
### 희웅 보완점
・ **팀 프로젝트 핵심 로직 최적화**

<img src="https://github.com/samhomesss/TeamProject/assets/159544864/98ec430a-83e1-4cc4-8386-81ce52c6f5bf" width="640" height="360">


・ **각 플레이어가 소유한 땅을 판정**

```rust
[이전 시도]

플레이어의 움직임은 포톤을 통해 동기화되었기에 맵은 플레이어의 위치를 바탕으로 땅의 소유권을 판별했습니다.

그러나 플레이어 수가 증가함에 따라 맵이 플레이어 수에 비례해 땅 소유권을 판별하는 계산을 해야 했고,
이로 인해 연산 부하가 증가하여 프레임 드랍이 심해졌습니다.

[최종 해결]

함수 동기화를 통해 플레이어의 땅 소유 정보를 실시간으로 전송하는 방식으로 개선했습니다.

이를 위해 전송되는 정보를 간소화하여 플레이어의 색상과 위치만 전달하도록 변경했습니다.

이로 인해 프레임이 크게 향상되고 연산 부하가 감소했습니다.

[결과]

Batch 수가 400에서 1418로 증가했음에도 불구하고 프레임은 110에서 180으로 개선되었습니다.
```
##
### 윤범 보완점
```scala
- 문제점 :

  Github-flow 전략을 기반으로 브랜치를 나눠 작업을 진행하였지만,
  프로그래머에게 있어 숙명과도 같은 충돌 문제를 회피하기엔 부족했습니다.

  특히, 리소스의 충돌이 자주 일어났습니다.

  코드간의 충돌은 결국 한쪽 코드를 수정하면 해결할 수 있는 문제였지만,
  모델 등 리소스의 충돌은 단순한 수정으로는 간단히 해결되는 문제가 아니였습니다.

- 해결 :

  1차적으로 브랜치를 나눠 작업을 함과 동시에,

  2차적으로 폴더를 나눠 각자의 리소스를 관리했습니다.

  이로인해 리소스가 겹칠 가능성 자체를 배제해 성공적으로 충돌 문제를 해결할 수 있었습니다.
```
##
### 승현 보완점
<img src="https://github.com/samhomesss/TeamProject/assets/159544864/4b39c2ae-2c3d-4526-b129-a8f25a7076ac" width="640" height="360">

・ 맵 판정 중 문제점 발생
```scala
[1번째 시도] - 이중배열을 이용해 노드 오브젝트를 생성하고 각 노드 들의 위치에 플레이어가 
               들어 온다면 색이 칠해 지도록 설정

      - 문제점 : Batches의 수가 17000개를 넘어감

[2번째 시도] - Batches가 17000개를 넘어가는 문제를 해결하기 위해
               하나의 텍스쳐를 가상의 Node라는 이중배열로 나눔

      - 문제점 : Batches 수는 4로 줄였지만 플레이어들 의 이름을 string으로 
                 받아와 비교할때 값을 많이 먹으며 Color값을 비교할 때  계속해서 
                 4개의 값을 비교하면서 비교해야하는 값이 많다 보니 안움직일때는 
                 FPS가 290인데  움직이면 FPS가 80으로 떨어짐

[최종 해결] - 플레이어들의 이름을 비교할때 string으로 사용하지 않고 플레이어를 Dictionary에 
              저장해서 비교하였으며 Color 값은 NodeColor라는 열거형 변수를 만들어서
              4개를 비교하는게 아닌 한가지 값을 비교 하는 것으로 바꿈
              FPS가 해결전은 80이었는데 190까지 올라감

```









