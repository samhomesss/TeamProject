<img width="1219" alt="스크린샷 2024-05-02 201050" src="https://github.com/samhomesss/TeamProject/assets/159544864/fc0728a4-56a8-4bb9-b9a3-070a4c2561d3">

<details><summary> 

  ## **목차**
  
</summary>
  
- [프로젝트 소개](#프로젝트-소개)
  
- [팀원 소개](#팀원-소개)
  
- [개발 환경](#개발-환경)

- [채택한 개발 기술과 브랜치 전략](#채택한-개발-기술과-브랜치-전략)

- [역할 분담](#역할-분담)

- [개발 기간 및 작업 관리](#개발-기간-및-작업-관리)

- [작업 중 문제점](#작업-중-문제점)
  

</details>

## **프로젝트 소개** 


## **팀원 소개** 

|  **이희웅**  |  **오윤범**  |  **윤승현**  |
|  ------  |  ------  |  ------  |
| [**@heewoung-lee**](https://github.com/heewoung-lee)  |  [**@oyb1412**](https://github.com/oyb1412) |  [**@Yoon.07.09**](https://github.com/samhomesss)  |

## **개발 환경** 


## **채택한 개발 기술과 브랜치 전략** 


## **역할 분담** 
**🍊이희웅**

- **DB(FireBase)**
    - 로그인
    - 회원가입
- **Server(Photon Pun2)**
    - 로비, 채팅창, 오브젝트 동기화

## **개발 기간 및 작업 관리** 

**개발 기간**

- 전체 개발 기간 : 2024-03-29 ~ 2024-04-22
- 기획:  2024-03-29 ~ 2024-04-01
- 프로토타입 개발 : 2024-04-02 ~ 2024-04-12
- 디버깅 및 폴리싱 : 2024-04-13 ~ 2024-04-22

**작업 관리**

- GitHub를 통한 형상관리

## **작업 중 문제점** 

- [희웅 작업중 문제점](#희웅-작업중-문제점)
- [윤범 작업중 문제점](#윤범-작업중-문제점)
- [승현 작업중 문제점](#승현-작업중-문제점)
### 희웅 작업중 문제점
### 윤범 작업중 문제점
### 승현 작업중 문제점
- 맵 판정 중 문제점 발생
```scala
       1번째 시도 - 이중배열을 이용해 노드 오브젝트를 생성하고 각 노드 들의 위치에 플레이어가 

                            들어 온다면 색이 칠해 지도록 설정

        

       - 문제점 : Batches의 수가 17000개를 넘어감

      

      2번째 시도 - Batches가 17000개를 넘어가는 문제를 해결하기 위해
                          하나의 텍스쳐를 가상의 Node라는 이중배열로 나눔

      - 문제점 : Batches 수는 4로 줄였지만 플레이어들 의 이름을 string으로 

                      받아와 비교할때 값을 많이 먹으며 Color값을 비교할 때  계속해서 

                      4개의 값을 비교하면서 비교해야하는 값이 많다 보니 안움직일때는 

                      FPS가 290인데  움직이면 FPS가 80으로 떨어짐

    최종 해결 - 플레이어들의 이름을 비교할때 string으로 사용하지 않고 플레이어를 Dictionary에 

                       저장해서 비교하였으며 Color 값은 NodeColor라는 열거형 변수를 만들어서 

                       4개를 비교하는게 아닌 한가지 값을 비교 하는 것으로 바꿈
                       FPS가 해결전은 80이었는데 190까지 올라감

```









