> 몇 가지의 스프라이트,사운드를 제외한 모든 리소스는 직접 제작하였습니다.
# Jumping Dungeon

![JUMPING DENGEON_Title](https://user-images.githubusercontent.com/61229227/96374205-310e4d00-11ac-11eb-9752-8f37818ccbb8.png)

플레이 영상
---
[![dd](https://img.youtube.com/vi/Bw16tWnNHKM/0.jpg)](https://www.youtube.com/watch?v=Bw16tWnNHKM?t=0s)

# 📘프로젝트 동기

모든 것을 혼자 책임지는 1인개발로 게임을 직접 만들어 보고 싶었습니다. 예전부터 게임개발은 버킷리스트 중 하나였습니다. 때문에 개발실력도 늘리고 해보고 싶은 것을 해보고 싶어서 이 프로젝트에 매진했습니다.

# ⌚️프로젝트 기간

_2019.12.29 ~ 2020.05.07_

# 💻프로젝트에 사용된 주요기술

* Unity(2019.2.16f1)
* C#(5.0)
* Aseprite
* Google Api
* Google AdMobs
    
# 📁간략한 폴더구조
> 간략하게 폴더의 구조와 설명을 포현했습니다.
>> 보안과 관련 있는 디렉토리는 업로드 하지 않았습니다.
* Assets
    - Animations (오브젝트 애니메이션 관련)
    - Prefabs (오브젝트의 프리팹들이 들어있다)
    - Scenes (게임 씬이 들어있음)
    - Scripts (cs파일들이 들어있다.)
* Pakages

# 📜기능

1. 싱글톤 디자인패턴을 적극적으로 적용한 인스턴스 최적화와 간편성(gameManager,googleManager,soundManager)
2. playerPrefs 로컬 스토어를 이용한 골드, 점수, 상점기능
3. googleApi를 이용한 구글로그인, 업적, 리더보드 기능
4. googleAdMobs를 이용한 보상형 광고
5. 오브젝트풀링 기법을 활용한 게임 최적화(리소스 재사용)
6. 코루틴을 활용한 수 십가지의 몬스터, 장애물, 아이템 패턴
7. 미터 수에 따른 난이도 변화
 
# 완성 모습

<img src = "https://user-images.githubusercontent.com/61229227/96376938-cfa2aa00-11bc-11eb-9dbd-0f159e3cf93f.png" width="40%">
<img src = "https://user-images.githubusercontent.com/61229227/96376940-d2050400-11bc-11eb-944a-4aca3aa6700f.png" width="40%">
<img src = "https://user-images.githubusercontent.com/61229227/96376942-d29d9a80-11bc-11eb-8fbf-0981d80d12f4.png" width="40%">
<img src = "https://user-images.githubusercontent.com/61229227/96376943-d3363100-11bc-11eb-94e4-2a05e40fbd7b.png" width="40%">
<img src = "https://user-images.githubusercontent.com/61229227/96376944-d4675e00-11bc-11eb-9727-91f7ff0c741d.png" width="40%">

# 👨🏼‍💻learned...

1. 앱 서비스의 기획~출시 까지의 과정을 통한 서비스 출시의 흐름
2. api 사용에 대한 이해
3. 싱글톤 디자인패턴의 활용법
4. 게임 최적화 기법 지식(코루틴,오브젝트풀링)
5. 서비스 내 광고 활용 방법등

# 아쉬운점들...

1. 혼자 해본 첫 프로젝트이다 보니 모든 것이 어려웠다.
2. 스크립팅 최적화를 별로 신경쓰지 못했다. (상속,파생클래스 등)
3. 직접 만든 리소스를 매우 고집하여 프로젝트 기간이 늘려졌다.
4. 깃허브를 사용하지 않았을때 했던 프로젝트라 버전관리가 안되어 위험했고 프로젝트에 대한 기억력이 흐려짐.

# 마치며

이 프로젝트를 마치며 얻은건 정말 많았습니다. 코드 자체는 다른 프로젝트보다 좋다고는 할 수 없지만 1인개발 서비스 출시라는 값진 경험은 돈 주고 살 수 없을 정도로 좋은 경험이었습니다.
